using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services
{
    public interface IActivitiesService
    {
        public Task<List<ActivityResponse>> GetAllActivitiesAsync();

        public Task<ActivityResponse?> GetActivityByID(int Id);

        public Task CreateAsync(ActivityRequest request);

        public Task UpdateAsync(int idActivities, ActivityRequest request);

        public Task<bool> DeleteAsync(int id);

    }
}
