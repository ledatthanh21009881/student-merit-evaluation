using Microsoft.EntityFrameworkCore;
using PJ_XET_THI_DUA_KHEN_THUONG.Data;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        
        // Định nghĩa các role dành cho sinh viên
        private readonly string[] _studentRoles = { "Student", "ClassLeader" };

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /**
         * Lấy danh sách sinh viên có đánh giá
         * @return Danh sách sinh viên có ít nhất một đánh giá
         */
        public async Task<List<Users>> GetStudentsWithEvaluationsAsync()
        {
            return await _context.Users
                .Include(u => u.Accounts)
                    .ThenInclude(a => a.Role)
                .Include(u => u.Evaluations)
                    .ThenInclude(e => e.CriteriaForm)
                .Where(u => u.Accounts.Any(a => _studentRoles.Contains(a.Role.RoleName)) && 
                           u.Evaluations.Any())
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ToListAsync();
        }

        /**
         * Lấy danh sách sinh viên có đánh giá với bộ lọc
         * @param semester Học kỳ để lọc
         * @param academicYear Năm học để lọc
         * @param status Trạng thái đánh giá để lọc
         * @return Danh sách sinh viên có ít nhất một đánh
         **/
        public async Task<List<Users>> GetStudentsWithEvaluationsFilterAsync(string? semester = null, int? academicYear = null, string? status = null)
        {
            var query = _context.Users
                .Include(u => u.Accounts)
                    .ThenInclude(a => a.Role)
                .Include(u => u.Evaluations)
                    .ThenInclude(e => e.CriteriaForm)
                .Where(u => u.Accounts.Any(a => _studentRoles.Contains(a.Role.RoleName)))
                .AsQueryable();

            // Lọc theo học kỳ
            if (!string.IsNullOrEmpty(semester))
            {
                query = query.Where(u => u.Evaluations.Any(e => e.Semester == semester));
            }

            // Lọc theo năm học
            if (academicYear.HasValue)
            {
                query = query.Where(u => u.Evaluations.Any(e => e.CriteriaForm.AcademicYearStart == academicYear.Value));
            }

            // Lọc theo trạng thái
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(u => u.Evaluations.Any(e => e.Status == status));
            }

            // Chỉ lấy những user có ít nhất 1 đánh giá
            query = query.Where(u => u.Evaluations.Any());

            return await query
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ToListAsync();
        }

        /**
         * Lấy chi tiết sinh viên có đánh giá
         * @param userId ID của sinh viên
         * @return Chi tiết sinh viên bao gồm thông tin tài khoản và đánh giá
         */
        public async Task<Users?> GetStudentWithEvaluationDetailsAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.Accounts)
                    .ThenInclude(a => a.Role)
                .Include(u => u.Evaluations)
                    .ThenInclude(e => e.CriteriaForm)
                .Include(u => u.Evaluations)
                    .ThenInclude(e => e.Details)
                        .ThenInclude(d => d.Criteria)
                .FirstOrDefaultAsync(u => u.UserID == userId && 
                                        u.Accounts.Any(a => _studentRoles.Contains(a.Role.RoleName)));
        }

        /**
         * Lấy danh sách tất cả sinh viên
         * @return Danh sách tất cả sinh viên
         */
        public async Task<List<Users>> GetAllStudentsAsync()
        {
            return await _context.Users
                .Include(u => u.Accounts)
                    .ThenInclude(a => a.Role)
                .Where(u => u.Accounts.Any(a => _studentRoles.Contains(a.Role.RoleName)))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ToListAsync();
        }

        /**
         * Kiểm tra xem người dùng có phải là sinh viên hay không
         * @param userId ID của người dùng
         * @return true nếu là sinh viên, false nếu không phải
         */
        public async Task<bool> IsStudentAsync(int userId)
        {
            return await _context.Users
                .Include(u => u.Accounts)
                    .ThenInclude(a => a.Role)
                .AnyAsync(u => u.UserID == userId && 
                              u.Accounts.Any(a => _studentRoles.Contains(a.Role.RoleName)));
        }
    }
}