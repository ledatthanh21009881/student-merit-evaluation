using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;
using PJ_XET_THI_DUA_KHEN_THUONG.Repositories;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services.Implement
{
    public class CriteriaFormService : ICriteriaFormService
    {
        private readonly ICriteriaFormRepository _criteriaFormRepository;
        private readonly ICriteriaRepository _criteriaRepository;
        private readonly IFormTimelineRepository _formTimelineRepository;

        public CriteriaFormService(
            ICriteriaFormRepository criteriaFormRepository,
            ICriteriaRepository criteriaRepository,
            IFormTimelineRepository formTimelineRepository)
        {
            _criteriaFormRepository = criteriaFormRepository;
            _criteriaRepository = criteriaRepository;
            _formTimelineRepository = formTimelineRepository;
        }

        private static CriteriaFormResponse MapToResponse(CriteriaForm form)
        {
            return new CriteriaFormResponse
            {
                CriteriaFormID = form.CriteriaFormID,
                FormName = form.FormName,
                AcademicYearStart = form.AcademicYearStart,
                Semester = form.Semester,
                Description = form.Description,
                StartDate = form.StartDate,
                EndDate = form.EndDate,
                FormType = form.FormType,
                IsActive = form.IsActive,

                // mapping danh sách tiêu chí đã chọn
                SelectedCriteria = form.Criterias?.Select(c => new CriteriaResponse
                {
                    CriteriaID = c.CriteriaID,
                    CriteriaName = c.CriteriaName,
                    Description = c.Description,
                    MaxScore = c.MaxScore,
                    Level = c.Level,
                    CriteriaTypeName = c.CriteriaType?.CriteriaTypeName ?? "",
                    IsStudentScored = c.IsStudentScored,
                    IsAdminScored = c.IsAdminScored,
                    IsUploadOnly = c.IsUploadOnly,
                    ParentID = c.ParentCriteria?.CriteriaID,
                    ParentName = c.ParentCriteria?.CriteriaName
                }).ToList() ?? new List<CriteriaResponse>(),

                // mapping timeline
                Timelines = form.FormTimelines?.Select(t => new FormTimelineResponse
                {
                    TimelineId = t.TimelineId,
                    StepName = t.StepName,
                    RoleTarget = t.RoleTarget,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    Description = t.Description
                }).ToList() ?? new List<FormTimelineResponse>()

            };
        }

        /**
         * Lấy tất cả form tiêu chí.
         * @return Trả về danh sách CriteriaFormResponse.
         */
        public async Task<List<CriteriaFormResponse>> GetAllAsync()
        {
            var forms = await _criteriaFormRepository.GetAllAsync();
            return forms.Select(MapToResponse).ToList();
        }

        /**
         * Lấy form tiêu chí theo ID (có chứa chi tiết các tiêu chí và timeline).
         * @param id ID của form tiêu chí cần lấy.
         * @return Trả về CriteriaFormResponse nếu tìm thấy, ngược lại trả về null.
         */
        public async Task<CriteriaFormResponse?> GetByIdAsync(int id)
        {
            var form = await _criteriaFormRepository.GetByIdWithDetailsAsync(id);
            if (form == null)
            {
                throw new Exception("Không tìm thấy form tiêu chí.");
            }

            return MapToResponse(form);
        }

        /**
         * Lấy danh sách tiêu chí theo danh sách ID.
         * @param criteriaIds Danh sách ID của tiêu chí cần lấy.
         * @return Trả về danh sách Criteria nếu tìm thấy, ngược lại trả về danh sách rỗng.
         */
        private async Task<List<Criteria>> GetCriteriaByIdsAsync(List<int> criteriaIds)
        {
            var allCriteria = await _criteriaRepository.GetAllCriteriaAsync(); // Lấy tất cả tiêu chí từ repository
            
            // Lọc tiêu chí theo danh sách ID
            return allCriteria.Where(c => criteriaIds.Contains(c.CriteriaID)).ToList();
        }

        public async Task CreateAsync(CriteriaFormRequest request)
        {
            // Kiểm tra trùng lặp
            if (await _criteriaFormRepository.ExistsByNameAsync(request.FormName, request.AcademicYearStart, request.Semester))
            {
                throw new Exception("Form với tên này đã tồn tại trong năm học và học kỳ tương ứng.");
            }

            // Kiểm tra ngày bắt đầu phải trước ngày kết thúc
            if (request.EndDate <= request.StartDate)
            {
                throw new Exception("Ngày kết thúc phải sau ngày bắt đầu.");
            }

            // Tạo form
            var criteriaForm = new CriteriaForm
            {
                FormName = request.FormName,
                AcademicYearStart = request.AcademicYearStart,
                Semester = request.Semester,
                Description = request.Description,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                FormType = request.FormType,
                IsActive = request.IsActive
            };

            // Thêm tiêu chí đã chọn
            if (request.SelectedCriteriaIds.Any())
            {
                var selectedCriteria = await GetCriteriaByIdsAsync(request.SelectedCriteriaIds);
                criteriaForm.Criterias = selectedCriteria;
            }

            await _criteriaFormRepository.AddAsync(criteriaForm);

            // Thêm timeline
            if (request.Timelines.Any())
            {
                foreach(var time in request.Timelines)
                {
                    // Kiểm tra ngày bắt đầu phải trước ngày kết thúc
                    if (time.EndDate <= time.StartDate)
                    {
                        throw new Exception("Ngày kết thúc phải sau ngày bắt đầu.");
                    }
                }

                var timelines = request.Timelines.Select(t => new FormTimeline
                {
                    CriteriaFormID = criteriaForm.CriteriaFormID,
                    StepName = t.StepName,
                    RoleTarget = t.RoleTarget,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    Description = t.Description
                }).ToList();

                await _formTimelineRepository.AddRangeAsync(timelines);
            }
        }

        public async Task UpdateAsync(int id, CriteriaFormRequest request)
        {
            var existingForm = await _criteriaFormRepository.GetByIdWithDetailsAsync(id);
            if (existingForm == null)
            {

                throw new Exception("Không tìm thấy form tiêu chí.");
            }

            // Kiểm tra trùng lặp (trừ bản ghi hiện tại)
            if (await _criteriaFormRepository.ExistsByNameAsync(request.FormName, request.AcademicYearStart, request.Semester, id))
            {

                throw new Exception("Form với tên này đã tồn tại trong năm học và học kỳ tương ứng.");
            }

            // Kiểm tra ngày bắt đầu phải trước ngày kết thúc
            if (request.EndDate <= request.StartDate)
            {
                throw new Exception("Ngày kết thúc phải sau ngày bắt đầu.");
            }


            // Cập nhật thông tin form
            existingForm.FormName = request.FormName;
            existingForm.AcademicYearStart = request.AcademicYearStart;
            existingForm.Semester = request.Semester;
            existingForm.Description = request.Description;
            existingForm.StartDate = request.StartDate;
            existingForm.EndDate = request.EndDate;
            existingForm.FormType = request.FormType;
            existingForm.IsActive = request.IsActive;

            // Cập nhật tiêu chí
            existingForm.Criterias.Clear(); // Xóa tất cả tiêu chí hiện tại
            if (request.SelectedCriteriaIds.Any())
            {
                var selectedCriteria = await GetCriteriaByIdsAsync(request.SelectedCriteriaIds);
                foreach (var criteria in selectedCriteria)
                {
                    existingForm.Criterias.Add(criteria);
                }
            }

            await _criteriaFormRepository.UpdateAsync(existingForm);

            // Cập nhật timeline
            await _formTimelineRepository.DeleteByFormIdAsync(id); // Xóa tất cả timeline hiện tại
            if (request.Timelines.Any())
            {
                foreach (var time in request.Timelines)
                {
                    // Kiểm tra ngày bắt đầu phải trước ngày kết thúc
                    if (time.EndDate <= time.StartDate)
                    {
                        throw new Exception("Ngày kết thúc phải sau ngày bắt đầu.");
                    }
                }

                var timelines = request.Timelines.Select(t => new FormTimeline
                {
                    CriteriaFormID = id,
                    StepName = t.StepName,
                    RoleTarget = t.RoleTarget,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    Description = t.Description
                }).ToList();

                await _formTimelineRepository.AddRangeAsync(timelines);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var form = await _criteriaFormRepository.GetByIdAsync(id);
            if (form == null)
                return false;

            // Xóa timeline trước
            await _formTimelineRepository.DeleteByFormIdAsync(id);

            // Xóa form
            await _criteriaFormRepository.DeleteAsync(form);
            return true;
        }

    }
}