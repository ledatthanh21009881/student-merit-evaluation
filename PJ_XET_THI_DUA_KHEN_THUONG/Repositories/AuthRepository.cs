// Repositories/AuthRepository.cs
using Microsoft.EntityFrameworkCore;
using PJ_XET_THI_DUA_KHEN_THUONG.Data;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
            => await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        public async Task<Student?> GetStudentByStudentCodeAsync(string studentCode)
            => await _context.Students.Include(s => s.User).FirstOrDefaultAsync(s => s.StudentCode == studentCode);
        public async Task<Student?> GetStudentByUserIdAsync(int userId)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.UserId == userId);
        }

        public async Task<bool> IsClassLeaderAsync(int studentId)
        {
            return await _context.ClassLeaders.AnyAsync(cl => cl.StudentId == studentId);
        }

        public async Task<Roles?> GetRoleAsync(int roleId)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == roleId);
        }


        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
