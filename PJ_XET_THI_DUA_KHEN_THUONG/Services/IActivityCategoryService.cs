using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services
{
    public interface IActivityCategoryService
    {
        Task<IEnumerable<ActivityCategoryResponse>> GetAllAsync();
        Task<ActivityCategoryResponse?> GetByIdAsync(int id);
        Task AddAsync(ActivityCategoryRequest request);
        Task<bool> UpdateAsync(int id, ActivityCategoryRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
