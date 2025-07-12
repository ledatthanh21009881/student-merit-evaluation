using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories
{
    public interface IActivityCategoryRepository
    {
        Task<IEnumerable<ActivityCategory>> GetAllAcitivityCategories();
        Task<ActivityCategory?> GetByIdAsync(int id);
        Task AddAsync(ActivityCategory criteria);
        Task UpdateAsync(ActivityCategory criteria);
        Task DeleteAsync(ActivityCategory criteria);
    }
}
