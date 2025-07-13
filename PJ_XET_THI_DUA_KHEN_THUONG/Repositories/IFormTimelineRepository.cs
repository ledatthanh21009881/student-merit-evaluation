using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories
{
    public interface IFormTimelineRepository
    {
        Task<List<FormTimeline>> GetByFormIdAsync(int criteriaFormId);
        Task AddRangeAsync(List<FormTimeline> timelines);
        Task AddAsync(FormTimeline timeline);
        Task DeleteByFormIdAsync(int criteriaFormId);
        Task UpdateAsync(FormTimeline timeline);
    }
}