using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services
{
    public interface IStudentEvaluationService
    {
        Task<List<EvaluationDto>> GetEvaluationFormAsync(int studentId, int semesterId, int academicYearId);
        Task<bool> SaveEvaluationAsync(SaveEvaluationRequestDto request);
        Task<bool> ConfirmEvaluationAsync(int studentId, int semesterId, int academicYearId);
    }
}
