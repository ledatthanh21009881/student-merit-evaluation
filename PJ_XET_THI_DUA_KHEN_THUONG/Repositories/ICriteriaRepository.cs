using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories
{
    public interface ICriteriaRepository
    {
        Task<IEnumerable<Criteria>> GetAllCriteriaAsync();
        Task<Criteria?> GetByIdAsync(int id);
        Task AddAsync(Criteria criteria);
        Task UpdateAsync(Criteria criteria);
        Task DeleteAsync(Criteria criteria);
        Task<List<Criteria>> GetByCriteriaTypeAsync(int criteriaTypeId, CancellationToken ct = default);

    }
}
