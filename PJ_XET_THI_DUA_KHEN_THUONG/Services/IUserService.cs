using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services
{
    public interface IUserService
    {
        Task<List<EvaluatedUserResponse>> GetEvaluatedStudentsAsync();
        Task<List<EvaluatedUserResponse>> GetStudentsAsync();

        Task<List<EvaluatedUserResponse>> GetEvaluatedStudentsFilterAsync(string? semester = null, int? academicYear = null, string? status = null);
        Task<EvaluatedUserResponse?> GetEvaluatedStudentDetailsAsync(int userId);
    }
}