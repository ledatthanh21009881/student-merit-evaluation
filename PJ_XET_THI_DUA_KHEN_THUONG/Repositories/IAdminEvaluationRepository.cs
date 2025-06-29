// IAdminEvaluationRepository.cs
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories
{
    public interface IAdminEvaluationRepository
    {
        Task SaveAdminEvaluationsAsync(AdminEvaluationDto dto, int evaluatedBy);
        Task<List<AdminEvaluationItemDto>> GetEvaluatedCriteriaAsync(int studentId, int setId);
        Task<List<AdminEvaluationItemDto>> GetCriteriaForAdminAsync(int setId);
    }
}