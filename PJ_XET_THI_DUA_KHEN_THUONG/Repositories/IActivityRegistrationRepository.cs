using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories
{
    public interface IActivityRegistrationRepository
    {
        Task<IEnumerable<ActivityRegistration>> GetAllAsync();
        Task<ActivityRegistration> GetByIdAsync(int activityId, int userId);
        Task AddAsync(ActivityRegistration registration);
        Task DeleteAsync(int activityId, int userId);
    }
}

