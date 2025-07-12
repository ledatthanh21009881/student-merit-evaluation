using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.EntityFrameworkCore;
using PJ_XET_THI_DUA_KHEN_THUONG.Data;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories.Implement
{
    public class ActivityCategoryRepository : IActivityCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public ActivityCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /**
         * Get all ActivityCategories
         * 
        */
        public async Task<IEnumerable<ActivityCategory>> GetAllAcitivityCategories()
        {
            return await _context.ActivityCategories.ToListAsync();
        }

        public async Task<ActivityCategory?> GetByIdAsync(int id)
        {
            return await _context.ActivityCategories.FindAsync(id);
        }

        public async Task AddAsync(ActivityCategory category)
        {
            _context.ActivityCategories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ActivityCategory category)
        {
            _context.ActivityCategories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ActivityCategory category)
        {
            _context.ActivityCategories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
