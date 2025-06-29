// AdminEvaluationRepository.cs
using PJ_XET_THI_DUA_KHEN_THUONG.Data;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories
{
    public class AdminEvaluationRepository : IAdminEvaluationRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminEvaluationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveAdminEvaluationsAsync(AdminEvaluationDto dto, int evaluatedBy)
        {
            // 1. Kiểm tra bộ tiêu chí có tồn tại không
            var setExists = await _context.EvaluationCriteriaSets.AnyAsync(x => x.SetId == dto.CriteriaSetId);
            if (!setExists)
            {
                throw new Exception($"Bộ tiêu chí với SetId = {dto.CriteriaSetId} không tồn tại.");
            }

            // 2. Lấy danh sách tiêu chí được phép chấm bởi Admin trong bộ tiêu chí đó
            var allowedCriteriaIds = await _context.EvaluationCriteriaSetDetails
                .Where(d => d.SetId == dto.CriteriaSetId)
                .Join(_context.EvaluationCriteriaMaster,
                      detail => detail.CriteriaId,
                      master => master.CriteriaId,
                      (detail, master) => new { detail.CriteriaId, master.IsScoredByAdmin })
                .Where(x => x.IsScoredByAdmin)
                .Select(x => x.CriteriaId)
                .ToListAsync();

            if (allowedCriteriaIds.Count == 0)
            {
                throw new Exception($"Không có tiêu chí nào được phép chấm bởi Admin trong bộ tiêu chí {dto.CriteriaSetId}.");
            }

            foreach (var eval in dto.Evaluations)
            {
                if (!allowedCriteriaIds.Contains(eval.CriteriaId))
                    continue; // bỏ qua tiêu chí không được phép

                var existing = await _context.AdminEvaluationDetails.FirstOrDefaultAsync(x =>
                    x.StudentId == dto.StudentId &&
                    x.CriteriaSetId == dto.CriteriaSetId &&
                    x.CriteriaId == eval.CriteriaId);

                if (existing == null)
                {
                    _context.AdminEvaluationDetails.Add(new AdminEvaluationDetails
                    {
                        StudentId = dto.StudentId,
                        CriteriaSetId = dto.CriteriaSetId,
                        CriteriaId = eval.CriteriaId,
                        Score = eval.Score,
                        Note = eval.Note,
                        EvaluatedBy = evaluatedBy,
                        EvaluatedAt = DateTime.Now
                    });
                }
                else
                {
                    existing.Score = eval.Score;
                    existing.Note = eval.Note;
                    existing.EvaluatedBy = evaluatedBy;
                    existing.EvaluatedAt = DateTime.Now;
                }
            }

            await _context.SaveChangesAsync();
        }


        public async Task<List<AdminEvaluationItemDto>> GetEvaluatedCriteriaAsync(int studentId, int setId)
        {
            return await _context.AdminEvaluationDetails
                .Where(x => x.StudentId == studentId && x.CriteriaSetId == setId)
                .Select(x => new AdminEvaluationItemDto
                {
                    CriteriaId = x.CriteriaId,
                    Score = x.Score,
                    Note = x.Note ?? ""
                }).ToListAsync();
        }

        public async Task<List<AdminEvaluationItemDto>> GetCriteriaForAdminAsync(int setId)
        {
            var query = from detail in _context.EvaluationCriteriaSetDetails
                        join master in _context.EvaluationCriteriaMaster
                        on detail.CriteriaId equals master.CriteriaId
                        where detail.SetId == setId && master.IsScoredByAdmin
                        select new AdminEvaluationItemDto
                        {
                            CriteriaId = master.CriteriaId,
                            CriteriaName = master.CriteriaName,
                            MaxScore = master.MaxScore,
                            Score = 0,
                            Note = ""
                        };

            return await query.ToListAsync();
        }
    }
}
