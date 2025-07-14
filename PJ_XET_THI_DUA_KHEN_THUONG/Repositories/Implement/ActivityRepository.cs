using Microsoft.EntityFrameworkCore;
using PJ_XET_THI_DUA_KHEN_THUONG.Data;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;
using System.Diagnostics;


namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories.Implement
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly ApplicationDbContext _context;

        public ActivityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Activites>> GetAllAsync()
        {
            return await _context.Activities.ToListAsync();
        }

        public async Task<Activites?> GetByIdAsync(int id)
        {
            return await _context.Activities.FirstOrDefaultAsync(a => a.ActivityId == id);
        }

        public async Task<Activites> AddAsync(Activites Activites)
        {
            _context.Activities.Add(Activites);
            await _context.SaveChangesAsync();
            return Activites;
        }

        public async Task<Activites?> UpdateAsync(int id, Activites Activites)
        {
            var existing = await _context.Activities.FindAsync(id);
            if (existing == null) return null;

            _context.Entry(existing).CurrentValues.SetValues(Activites);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Activities.FindAsync(id);
            if (existing == null) return false;

            _context.Activities.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        Task<IEnumerable<Activites>> IActivityRepository.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Activites?> IActivityRepository.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Activity> AddAsync(Activity activity)
        {
            throw new NotImplementedException();
        }

        public Task<Activity?> UpdateAsync(int id, Activity activity)
        {
            throw new NotImplementedException();
        }
    }
}
