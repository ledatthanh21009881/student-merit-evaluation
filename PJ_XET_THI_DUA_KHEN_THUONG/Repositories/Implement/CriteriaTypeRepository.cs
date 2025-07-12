using Microsoft.EntityFrameworkCore;
using PJ_XET_THI_DUA_KHEN_THUONG.Data;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories.Implement
{
    public class CriteriaTypeRepository : ICriteriaTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public CriteriaTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CriteriaType>> GetAllAsync()
        {
            return await _context.CriteriaTypes
                .Include(ct => ct.CriteriaList)
                .ToListAsync();
        }

        public async Task<CriteriaType?> GetByIdAsync(int id)
        {
            return await _context.CriteriaTypes
                .FirstOrDefaultAsync(ct => ct.CriteriaTypeID == id);
        }

        public async Task<CriteriaType?> GetByIdWithCriteriaAsync(int id)
        {
            return await _context.CriteriaTypes
                .Include(ct => ct.CriteriaList)
                .FirstOrDefaultAsync(ct => ct.CriteriaTypeID == id);
        }

        public async Task AddAsync(CriteriaType criteriaType)
        {
            _context.CriteriaTypes.Add(criteriaType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(CriteriaType criteriaType)
        {
            _context.CriteriaTypes.Update(criteriaType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(CriteriaType criteriaType)
        {
            _context.CriteriaTypes.Remove(criteriaType);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.CriteriaTypes
                .AnyAsync(ct => ct.CriteriaTypeName.ToLower() == name.ToLower());
        }

        public async Task<bool> ExistsByNameAsync(string name, int excludeId)
        {
            return await _context.CriteriaTypes
                .AnyAsync(ct => ct.CriteriaTypeName.ToLower() == name.ToLower() && ct.CriteriaTypeID != excludeId);
        }
    }
}