using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories
{
    public interface IStudentEvaluationRepository
    {
        Task<List<EvaluationDto>> GetEvaluationFormAsync(int studentId, int semesterId, int academicYearId);
        Task<bool> SaveEvaluationAsync(SaveEvaluationRequestDto request);
        Task<bool> ConfirmEvaluationAsync(int studentId, int semesterId, int academicYearId);
    }
}
