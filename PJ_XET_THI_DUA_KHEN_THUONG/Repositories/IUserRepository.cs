using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories
{
    public interface IUserRepository
    {
        Task<List<Users>> GetStudentsWithEvaluationsAsync();
        Task<List<Users>> GetStudentsWithEvaluationsFilterAsync(string? semester = null, int? academicYear = null, string? status = null);
        Task<Users?> GetStudentWithEvaluationDetailsAsync(int userId);
        Task<List<Users>> GetAllStudentsAsync();
        Task<bool> IsStudentAsync(int userId);
    }
}