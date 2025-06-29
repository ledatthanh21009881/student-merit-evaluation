// IAdminEvaluationService.cs
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services
{
    public interface IAdminEvaluationService
    {
        Task SaveAdminEvaluationsAsync(AdminEvaluationDto dto, int adminId);
        Task<List<AdminEvaluationItemDto>> GetEvaluatedCriteriaAsync(int studentId, int setId);
        Task<List<AdminEvaluationItemDto>> GetCriteriaForAdminAsync(int setId);
    }
}