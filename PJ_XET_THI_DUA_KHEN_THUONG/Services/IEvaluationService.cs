using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services
{
    public interface IEvaluationService
    {
        Task<List<EvaluationResponse>> GetAllAsync();
        Task<EvaluationResponse?> GetByIdAsync(int id);
        Task<EvaluationResponse?> GetByUserFormSemesterAsync(int userId, int formId, int semester);
        Task AddOrUpdateAsync(EvaluationRequest request);
        Task DeleteAsync(int id);
    }
} 