using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services
{
    public interface ICriteriaTypeService
    {
        Task<List<CriteriaTypeResponse>> GetAllAsync();
        Task<CriteriaTypeResponse?> GetByIdAsync(int id);
        Task CreateAsync(CriteriaTypeRequest request);
        Task UpdateAsync(int id, CriteriaTypeRequest request);
        Task<bool> DeleteAsync(int id);
    }
}