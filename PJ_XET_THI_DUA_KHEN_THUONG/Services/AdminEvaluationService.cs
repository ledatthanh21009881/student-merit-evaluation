// AdminEvaluationService.cs
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs;
using PJ_XET_THI_DUA_KHEN_THUONG.Repositories;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services
{
    public class AdminEvaluationService : IAdminEvaluationService
    {
        private readonly IAdminEvaluationRepository _repository;

        public AdminEvaluationService(IAdminEvaluationRepository repository)
        {
            _repository = repository;
        }

        public Task SaveAdminEvaluationsAsync(AdminEvaluationDto dto, int adminId)
        {
            return _repository.SaveAdminEvaluationsAsync(dto, adminId);
        }

        public Task<List<AdminEvaluationItemDto>> GetEvaluatedCriteriaAsync(int studentId, int setId)
        {
            return _repository.GetEvaluatedCriteriaAsync(studentId, setId);
        }

        public Task<List<AdminEvaluationItemDto>> GetCriteriaForAdminAsync(int setId)
        {
            return _repository.GetCriteriaForAdminAsync(setId);
        }
    }
}