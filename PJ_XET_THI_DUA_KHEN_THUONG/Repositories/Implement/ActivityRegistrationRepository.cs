using PJ_XET_THI_DUA_KHEN_THUONG.Data;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories.Implement
{
    public class ActivityRegistrationRepository : IActivityRegistrationRepository
    {
        private readonly ApplicationDbContext _context;

        public ActivityRegistrationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ActivityRegistration>> GetAllAsync()
        {
            return await _context.ActivityRegistrations.ToListAsync();
        }

        public async Task<ActivityRegistration> GetByIdAsync(int activityId, int userId)
        {
            return await _context.ActivityRegistrations
                .FirstOrDefaultAsync(ar => ar.ActivityId == activityId && ar.UserID == userId);
        }

        public async Task AddAsync(ActivityRegistration registration)
        {
            _context.ActivityRegistrations.Add(registration);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int activityId, int userId)
        {
            var registration = await GetByIdAsync(activityId, userId);
            if (registration != null)
            {
                _context.ActivityRegistrations.Remove(registration);
                await _context.SaveChangesAsync();
            }
        }
    }
}
