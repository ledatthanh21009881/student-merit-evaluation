using PJ_XET_THI_DUA_KHEN_THUONG.Data;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories.Implement
{
    public class EvaluationRepository : IEvaluationRepository
    {
        private readonly ApplicationDbContext _context;
        public EvaluationRepository(ApplicationDbContext context) { _context = context; }

        public async Task<IEnumerable<Evaluations>> GetAllAsync() => await _context.Evaluations.Include(e => e.Details).ToListAsync();
        public async Task<Evaluations?> GetByIdAsync(int id) => await _context.Evaluations.Include(e => e.Details).FirstOrDefaultAsync(e => e.EvaluationsID == id);
        public async Task<Evaluations?> GetByUserFormSemesterAsync(int userId, int formId, string semester) => await _context.Evaluations.Include(e => e.Details).FirstOrDefaultAsync(e => e.UserID == userId && e.CriteriaFormID == formId && e.Semester == semester);
        public async Task AddAsync(Evaluations evaluation)
        {
            _context.Evaluations.Add(evaluation);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Evaluations evaluation)
        {
            _context.Evaluations.Update(evaluation);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Evaluations
                .Include(e => e.Details)
                .FirstOrDefaultAsync(e => e.EvaluationsID == id);
            if (entity != null)
            {
                // Xóa tất cả EvaluationDetails trước
                _context.EvaluationDetails.RemoveRange(entity.Details);
                _context.Evaluations.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public IQueryable<Evaluations> GetQueryable() => _context.Evaluations;
    }
} 