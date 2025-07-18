using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories
{
    public interface IEvaluationRepository
    {
        Task<IEnumerable<Evaluations>> GetAllAsync();
        Task<Evaluations?> GetByIdAsync(int id);
        Task<Evaluations?> GetByUserFormSemesterAsync(int userId, int formId, string semester);
        Task AddAsync(Evaluations evaluation);
        Task UpdateAsync(Evaluations evaluation);
        Task DeleteAsync(int id);
        IQueryable<Evaluations> GetQueryable();
    }
} 