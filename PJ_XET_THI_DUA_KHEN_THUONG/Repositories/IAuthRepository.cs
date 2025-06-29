// Repositories/IAuthRepository.cs
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByUsernameAsync(string username);
        Task<Student?> GetStudentByUserIdAsync(int userId);
        Task<bool> IsClassLeaderAsync(int studentId);
        Task<Roles?> GetRoleAsync(int roleId);
        Task<Student?> GetStudentByStudentCodeAsync(string studentCode);
    }

}
