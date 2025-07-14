using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services
{
    public interface ICriteriaFormService
    {
        Task<List<CriteriaFormResponse>> GetAllAsync();
        Task<CriteriaFormResponse?> GetByIdAsync(int id);
        Task CreateAsync(CriteriaFormRequest request);
        Task UpdateAsync(int id, CriteriaFormRequest request);
        Task<bool> DeleteAsync(int id);

    }
}