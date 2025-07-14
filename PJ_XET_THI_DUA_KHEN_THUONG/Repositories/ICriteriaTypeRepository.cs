using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories
{
    public interface ICriteriaTypeRepository
    {
        Task<IEnumerable<CriteriaType>> GetAllAsync();
        Task<CriteriaType?> GetByIdAsync(int id);
        Task AddAsync(CriteriaType criteriaType);
        Task UpdateAsync(CriteriaType criteriaType);
        Task DeleteAsync(CriteriaType criteriaType);
        public Task<bool> ExistsByNameAsync(string name);
        public Task<bool> ExistsByNameAsync(string name, int excludeId);
    }
}