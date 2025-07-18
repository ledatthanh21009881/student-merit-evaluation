using Microsoft.EntityFrameworkCore;
using PJ_XET_THI_DUA_KHEN_THUONG.Data;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories.Implement
{
    public class CriteriaFormRepository : ICriteriaFormRepository
    {
        private readonly ApplicationDbContext _context;

        public CriteriaFormRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CriteriaForm>> GetAllAsync()
        {
            return await _context.CriteriaForms
                .Include(cf => cf.Criterias)
                    .ThenInclude(c => c.CriteriaType)
                .Include(cf => cf.FormTimelines)
                //.OrderByDescending(cf => cf.CriteriaFormID)
                .ToListAsync();
        }

        /**
         * Lấy CriteriaForm theo ID.
         * @param id ID của CriteriaForm cần lấy.
         * @return Trả về CriteriaForm nếu tìm thấy, ngược lại trả về null.
         */
        public async Task<CriteriaForm?> GetByIdAsync(int id)
        {
            return await _context.CriteriaForms
                .FirstOrDefaultAsync(cf => cf.CriteriaFormID == id);
        }

        /**
         * Lấy CriteriaForm theo ID và bao gồm các chi tiết liên quan như Criterias và FormTimelines.
         * @param id ID của CriteriaForm cần lấy.
         * @return Trả về CriteriaForm nếu tìm thấy, ngược lại trả về null.
         */
        public async Task<CriteriaForm?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.CriteriaForms
                .Include(cf => cf.Criterias) // lấy thông tin tiêu chí
                    .ThenInclude(c => c.CriteriaType) // lấy thông tin loại tiêu chí
                .Include(cf => cf.FormTimelines) // lấy thông tin timeline của form
                .FirstOrDefaultAsync(cf => cf.CriteriaFormID == id);
        }

        public async Task AddAsync(CriteriaForm criteriaForm)
        {
            _context.CriteriaForms.Add(criteriaForm);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CriteriaForm criteriaForm)
        {
            _context.CriteriaForms.Update(criteriaForm);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CriteriaForm criteriaForm)
        {
            _context.CriteriaForms.Remove(criteriaForm);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByNameAsync(string formName, int academicYear, string semester)
        {
            return await _context.CriteriaForms
                .AnyAsync(cf => cf.FormName.ToLower() == formName.ToLower() 
                            && cf.AcademicYearStart == academicYear 
                            && cf.Semester.ToLower() == semester.ToLower());
        }

        /**
         * Kiểm tra xem tên biểu mẫu đã tồn tại hay chưa, ngoại trừ ID đã cho
        **/
        public async Task<bool> ExistsByNameAsync(string formName, int academicYear, string semester, int excludeId)
        {
            return await _context.CriteriaForms
                .AnyAsync(cf => cf.FormName.ToLower() == formName.ToLower() 
                            && cf.AcademicYearStart == academicYear 
                            && cf.Semester.ToLower() == semester.ToLower()
                            && cf.CriteriaFormID != excludeId);
        }

        /**
         * Lấy danh sách các biểu mẫu đang hoạt động
         * @return Danh sách các biểu mẫu CriteriaForm đang hoạt động
         * */
        public async Task<List<CriteriaForm>> GetActiveFormsAsync()
        {
            return await _context.CriteriaForms
                .Where(cf => cf.IsActive)
                .Include(cf => cf.Criterias)
                .Include(cf => cf.FormTimelines)
                .ToListAsync();
        }

        /**
         * Lấy danh sách các biểu mẫu theo năm học
         * @param academicYear Năm học để lọc
         * @return Danh sách các biểu mẫu CriteriaForm theo năm học
         */
        public async Task<List<CriteriaForm>> GetByAcademicYearAsync(int academicYear)
        {
            return await _context.CriteriaForms
                .Where(cf => cf.AcademicYearStart == academicYear)
                .Include(cf => cf.Criterias)
                .Include(cf => cf.FormTimelines)
                .ToListAsync();
        }

        public async Task RemoveCriteriaLinksAsync(int criteriaFormId)
        {
            var form = await _context.CriteriaForms
                .Include(cf => cf.Criterias)
                .FirstOrDefaultAsync(cf => cf.CriteriaFormID == criteriaFormId);
            
            if (form != null && form.Criterias.Any())
            {
                form.Criterias.Clear();
                await _context.SaveChangesAsync();
            }
        }

        /**
         * Lọc form tiêu chí theo năm hoặc học kỳ
         */
        public async Task<List<CriteriaForm>> GetByFilterAsync(int? academicYear, string? semester)
        {
            var query = _context.CriteriaForms
                .Include(cf => cf.Criterias)
                    .ThenInclude(c => c.CriteriaType)
                .Include(cf => cf.FormTimelines)
                .AsQueryable();

            // Lọc theo năm học nếu có
            if (academicYear.HasValue)
            {
                query = query.Where(cf => cf.AcademicYearStart == academicYear.Value);
            }

            // Lọc theo học kỳ nếu có
            if (!string.IsNullOrEmpty(semester))
            {
                query = query.Where(cf => cf.Semester.ToLower() == semester.ToLower());
            }

            return await query.ToListAsync();
        }

        public async Task<List<int>> GetCriteriaIdsByFormIdAsync(int formId)
        {
            return await _context.CriteriaForms
                .Where(cf => cf.CriteriaFormID == formId)
                .SelectMany(cf => cf.Criterias.Select(c => c.CriteriaID))
                .ToListAsync();
        }


    }
}