using Microsoft.EntityFrameworkCore;
using PJ_XET_THI_DUA_KHEN_THUONG.Data;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories.Implement
{
    public class FormTimelineRepository : IFormTimelineRepository
    {
        private readonly ApplicationDbContext _context;

        public FormTimelineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<FormTimeline>> GetByFormIdAsync(int criteriaFormId)
        {
            return await _context.FormTimelines
                .Where(ft => ft.CriteriaFormID == criteriaFormId)
                .OrderBy(ft => ft.StartDate)
                .ToListAsync();
        }

        /**
         * Thêm một FormTimeline vào cơ sở dữ liệu.
         **/
        public async Task AddAsync(FormTimeline timeline)
        {
            _context.FormTimelines.Add(timeline);
            await _context.SaveChangesAsync();
        }

        /**
         * Thêm một danh sách các FormTimeline vào cơ sở dữ liệu.
         **/
        public async Task AddRangeAsync(List<FormTimeline> timelines)
        {
            _context.FormTimelines.AddRange(timelines);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByFormIdAsync(int criteriaFormId)
        {
            // Lấy tất cả FormTimelines theo CriteriaFormID
            var timelines = await _context.FormTimelines
                .Where(ft => ft.CriteriaFormID == criteriaFormId)
                .ToListAsync();

            _context.FormTimelines.RemoveRange(timelines);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FormTimeline timeline)
        {
            _context.FormTimelines.Update(timeline);
            await _context.SaveChangesAsync();
        }

    }
}