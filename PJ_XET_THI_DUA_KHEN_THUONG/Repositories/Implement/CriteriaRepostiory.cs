using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.EntityFrameworkCore;
using PJ_XET_THI_DUA_KHEN_THUONG.Data;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories.Implement
{
    public class CriteriaRepostiory : ICriteriaRepository
    {
        private readonly ApplicationDbContext _context;

        public CriteriaRepostiory(ApplicationDbContext context)
        {
            _context = context;
        }

        /**
         * Get all criteria
         * 
        */
        public async Task<IEnumerable<Criteria>> GetAllCriteriaAsync()
        {
            return await _context.Criteria
                .Include(c => c.CriteriaType)
                .Include(c => c.SubCriteria)
                .ToListAsync();
        }

        public async Task AddAsync(Criteria criteria)
        {
            _context.Criteria.Add(criteria);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(Criteria criteria)
        {
            _context.Criteria.Remove(criteria);
            await _context.SaveChangesAsync();
        }

        public async Task<Criteria?> GetByIdAsync(int id)
        {
            return await _context.Criteria.FindAsync(id);
        }

        public async Task UpdateAsync(Criteria entity)
        {
            _context.Criteria.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
