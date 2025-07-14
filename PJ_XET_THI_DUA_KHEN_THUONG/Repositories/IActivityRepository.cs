using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories
{
    public interface IActivityRepository
    {
        Task<IEnumerable<Activites>> GetAllAsync(); // Fixed missing '>'
        Task<Activites?> GetByIdAsync(int id);
        Task<Activites> AddAsync(Activites activity);
        Task<Activites?> UpdateAsync(int id, Activites activity);
        Task<bool> DeleteAsync(int id);
    }
}

