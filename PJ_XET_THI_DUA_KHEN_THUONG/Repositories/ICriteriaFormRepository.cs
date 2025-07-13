using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories
{
    public interface ICriteriaFormRepository
    {
        Task<IEnumerable<CriteriaForm>> GetAllAsync();
        Task<CriteriaForm?> GetByIdAsync(int id);
        Task<CriteriaForm?> GetByIdWithDetailsAsync(int id);
        Task AddAsync(CriteriaForm criteriaForm);
        Task UpdateAsync(CriteriaForm criteriaForm);
        Task DeleteAsync(CriteriaForm criteriaForm);
        Task<bool> ExistsByNameAsync(string formName, int academicYear, string semester);
        Task<bool> ExistsByNameAsync(string formName, int academicYear, string semester, int excludeId);
    }
}