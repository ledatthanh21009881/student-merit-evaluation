using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Vml.Office;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;
using PJ_XET_THI_DUA_KHEN_THUONG.Repositories;
using System.Linq;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services.Implement
{
    public class CriteriaService : ICriteriaService
    {
        private readonly ICriteriaRepository _criteriaRepository;
        public CriteriaService(ICriteriaRepository criteriaRepository)
        {
            _criteriaRepository = criteriaRepository;
        }

        public async Task<List<CriteriaResponse>> GetCriteriaTreeAsync()
        {
            var all = await _criteriaRepository.GetAllCriteriaAsync(); // có Include()

            var lookup = all.ToLookup(c => c.ParentCriteria?.CriteriaID);

            List<CriteriaResponse> BuildTree(int? parentId)
            {
                return lookup[parentId]
                    .Select(c => new CriteriaResponse
                    {
                        CriteriaID = c.CriteriaID,
                        CriteriaName = c.CriteriaName,
                        Description = c.Description,
                        MaxScore = c.MaxScore,
                        Level = c.Level,
                        IsStudentScored = c.IsStudentScored,
                        IsAdminScored = c.IsAdminScored,
                        IsUploadOnly = c.IsUploadOnly,
                        IsActive = c.IsActive,
                        CriteriaTypeName = c.CriteriaType?.CriteriaTypeName,
                        Children = BuildTree(c.CriteriaID)
                    })
                    .ToList();
            }

            return BuildTree(null); // Bắt đầu từ cha gốc
        }

        public async Task CreateAsync(CriteriaRequest request)
        {
            var entity = new Criteria
            {
                CriteriaName = request.CriteriaName,
                Description = request.Description,
                MaxScore = request.MaxScore,
                Level = request.Level,
                IsStudentScored = request.IsStudentScored,
                IsAdminScored = request.IsAdminScored,
                IsUploadOnly = request.IsUploadOnly,
                IsActive = request.IsActive,
                CriteriaTypeID = request.CriteriaTypeID,
                ParentID = request.ParentID
            };

            //Kiểm tra xem thử tiêu chí cha có tồn tại không
            if (request.ParentID.HasValue)
            {
                var parent = await _criteriaRepository.GetByIdAsync(request.ParentID.Value);
                if (parent == null)
                    throw new Exception("Không tìm thấy tiêu chí cha.");
            }

            await _criteriaRepository.AddAsync(entity);
        }

        public async Task<CriteriaResponse?> GetCriteriaByID(int Id)
        {
            var criteria = await _criteriaRepository.GetByIdAsync(Id);
            if(criteria == null)
            {
                throw new Exception("Không tìm thấy tiêu chí nào.");
            }

            return new CriteriaResponse
            {
                CriteriaID = criteria.CriteriaID,
                CriteriaName = criteria.CriteriaName,
                Description = criteria.Description,
                MaxScore = criteria.MaxScore,
                Level = criteria.Level,
                IsStudentScored = criteria.IsStudentScored,
                IsAdminScored = criteria.IsAdminScored,
                IsUploadOnly = criteria.IsUploadOnly,
                IsActive = criteria.IsActive,
                ParentID = criteria.ParentCriteria?.ParentID,
                ParentName = criteria.ParentCriteria?.CriteriaName,
                CriteriaTypeName = criteria.CriteriaType?.CriteriaTypeName ?? ""
            };
        }
    }
}
