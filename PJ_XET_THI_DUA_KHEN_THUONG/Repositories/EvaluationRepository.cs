//using Microsoft.EntityFrameworkCore;
//using PJ_XET_THI_DUA_KHEN_THUONG.Data;
//using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs;
//using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

//namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories
//{
//    public class EvaluationRepository : IEvaluationRepository
//    {
//        private readonly ApplicationDbContext _context;

//        public EvaluationRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<List<EvaluationDto>> GetEvaluationsByStudentAsync(int studentId, int semesterId, int academicYearId)
//        {
//            var evaluation = await _context.Evaluations
//                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.SemesterId == semesterId);

//            if (evaluation == null) return new List<EvaluationDto>();

//            var details = await _context.EvaluationDetails
//                .Where(d => d.EvaluationId == evaluation.EvaluationId)
//                .ToListAsync();

//            var notes = await _context.EvaluationNotes
//                .Where(n => n.EvaluationId == evaluation.EvaluationId && n.EvaluatedByRole == 3)
//                .ToListAsync();

//            var criteriaList = await _context.EvaluationCriteriaMaster.ToListAsync();

//            var result = from c in criteriaList
//                         join d in details on c.CriteriaId equals d.CriteriaId into cd
//                         from detail in cd.DefaultIfEmpty()
//                         join n in notes on c.CriteriaId equals n.CriteriaId into cn
//                         from note in cn.DefaultIfEmpty()
//                         select new EvaluationDto
//                         {
//                             EvaluationId = evaluation.EvaluationId,
//                             CriteriaId = c.CriteriaId,
//                             CriteriaName = c.CriteriaName,
//                             MaxScore = c.MaxScore,
//                             StudentScore = detail?.StudentScore ?? 0,
//                             Note = note?.Note ?? string.Empty
//                         };

//            return result.ToList();
//        }

//        public async Task<bool> SaveStudentEvaluationAsync(int studentId, int semesterId, int academicYearId, List<EvaluationInputDto> evaluations)
//        {
//            using var transaction = await _context.Database.BeginTransactionAsync();

//            try
//            {
//                var evaluation = await _context.Evaluations
//                    .FirstOrDefaultAsync(e => e.StudentId == studentId && e.SemesterId == semesterId);

//                if (evaluation == null)
//                {
//                    evaluation = new Evaluations
//                    {
//                        StudentId = studentId,
//                        SemesterId = semesterId,
//                        Status = "Pending",
//                        SubmittedAt = DateTime.Now
//                    };
//                    _context.Evaluations.Add(evaluation);
//                    await _context.SaveChangesAsync();
//                }

//                int evaluationId = evaluation.EvaluationId;

//                foreach (var item in evaluations)
//                {
//                    var criteria = await _context.EvaluationCriteriaMaster
//                        .FirstOrDefaultAsync(c => c.CriteriaId == item.CriteriaId);

//                    if (criteria == null) continue;

//                    var detail = await _context.EvaluationDetails
//                        .FirstOrDefaultAsync(d => d.EvaluationId == evaluationId && d.CriteriaId == item.CriteriaId);

//                    if (detail == null)
//                    {
//                        detail = new EvaluationDetails
//                        {
//                            EvaluationId = evaluationId,
//                            CriteriaId = item.CriteriaId,
//                            StudentScore = criteria.IsScoredByStudent ? Math.Min(item.Score, criteria.MaxScore) : 0,
//                            ClassLeaderScore = (criteria.IsScoredByStudent) ? Math.Min(item.Score, criteria.MaxScore) : null
//                        };
//                        _context.EvaluationDetails.Add(detail);
//                    }
//                    else
//                    {
//                        detail.StudentScore = criteria.IsScoredByStudent ? Math.Min(item.Score, criteria.MaxScore) : detail.StudentScore;
//                        if (criteria.IsScoredByStudent)
//                        {
//                            detail.ClassLeaderScore = Math.Min(item.Score, criteria.MaxScore);
//                        }
//                        _context.EvaluationDetails.Update(detail);
//                    }

//                    if (!string.IsNullOrEmpty(item.Note))
//                    {
//                        _context.EvaluationNotes.Add(new EvaluationNotes
//                        {
//                            EvaluationId = evaluationId,
//                            CriteriaId = item.CriteriaId,
//                            Note = item.Note,
//                            EvaluatedBy = studentId,
//                            EvaluatedByRole = 3,
//                            EvaluatedAt = DateTime.Now
//                        });
//                    }
//                }

//                await _context.SaveChangesAsync();
//                await transaction.CommitAsync();
//                return true;
//            }
//            catch
//            {
//                await transaction.RollbackAsync();
//                throw;
//            }
//        }

//        public async Task<bool> ConfirmStudentEvaluationAsync(int studentId, int semesterId, int academicYearId)
//        {
//            var evaluation = await _context.Evaluations
//                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.SemesterId == semesterId);

//            if (evaluation == null) return false;

//            evaluation.Status = "Submitted";
//            evaluation.SubmittedAt = DateTime.Now;

//            _context.Evaluations.Update(evaluation);
//            await _context.SaveChangesAsync();

//            return true;
//        }
//    }
//}
