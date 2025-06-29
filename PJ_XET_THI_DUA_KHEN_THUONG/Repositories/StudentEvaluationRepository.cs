using Microsoft.EntityFrameworkCore;
using PJ_XET_THI_DUA_KHEN_THUONG.Data;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories
{
    public class StudentEvaluationRepository : IStudentEvaluationRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentEvaluationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<EvaluationDto>> GetEvaluationFormAsync(int studentId, int semesterId, int academicYearId)
        {
            if (semesterId <= 0 || academicYearId <= 0)
                throw new ArgumentException("Cần chọn học kỳ và năm học.");

            var criteriaSet = await _context.EvaluationCriteriaSets
                .FirstOrDefaultAsync(x => x.SemesterId == semesterId && x.AcademicYearId == academicYearId);

            if (criteriaSet == null) return new List<EvaluationDto>();

            var criteriaDetails = await (from detail in _context.EvaluationCriteriaSetDetails
                                         join master in _context.EvaluationCriteriaMaster on detail.CriteriaId equals master.CriteriaId
                                         where detail.SetId == criteriaSet.SetId
                                         select new
                                         {
                                             master.CriteriaId,
                                             master.CriteriaName,
                                             master.Description,
                                             master.MaxScore,
                                             master.IsScoredByStudent,
                                             master.IsUploadOnly
                                         }).ToListAsync();

            var evaluation = await _context.Evaluations
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.SemesterId == semesterId);

            int evaluationId = evaluation?.EvaluationId ?? 0;

            var details = await _context.EvaluationDetails
                .Where(x => x.EvaluationId == evaluationId)
                .ToDictionaryAsync(x => x.CriteriaId);

            var notes = await _context.EvaluationNotes
                .Where(x => x.EvaluationId == evaluationId)
                .ToDictionaryAsync(x => x.CriteriaId, x => x.Note);

            var evidences = await _context.EvaluationEvidences
                .Where(x => x.EvaluationId == evaluationId)
                .ToDictionaryAsync(x => x.CriteriaId, x => x.FilePath);

            var adminScores = await _context.AdminEvaluationDetails
                .Where(x => x.StudentId == studentId && x.CriteriaSetId == criteriaSet.SetId)
                .ToDictionaryAsync(x => x.CriteriaId, x => new { x.Score, x.Note });

            var result = criteriaDetails.Select(c => new EvaluationDto
            {
                CriteriaId = c.CriteriaId,
                CriteriaName = c.CriteriaName,
                Description = c.Description,
                MaxScore = c.MaxScore,
                IsScoredByStudent = c.IsScoredByStudent,
                IsUploadOnly = c.IsUploadOnly,
                StudentScore = details.ContainsKey(c.CriteriaId) ? details[c.CriteriaId].StudentScore : null,
                StudentNote = notes.ContainsKey(c.CriteriaId) ? notes[c.CriteriaId] : null,
                EvidencePath = evidences.ContainsKey(c.CriteriaId) ? evidences[c.CriteriaId] : null,
                AdminScore = adminScores.ContainsKey(c.CriteriaId) ? adminScores[c.CriteriaId].Score : null,
                AdminNote = adminScores.ContainsKey(c.CriteriaId) ? adminScores[c.CriteriaId].Note : null
            }).ToList();

            return result;
        }

        public async Task<bool> SaveEvaluationAsync(SaveEvaluationRequestDto request)
        {
            if (request.SemesterId <= 0 || request.AcademicYearId <= 0)
                return false;

            var evaluation = await _context.Evaluations
                .FirstOrDefaultAsync(e => e.StudentId == request.StudentId && e.SemesterId == request.SemesterId);

            if (evaluation == null)
            {
                evaluation = new Evaluations
                {
                    StudentId = request.StudentId,
                    SemesterId = request.SemesterId,
                    Status = "Pending",
                    SubmittedAt = null
                };
                _context.Evaluations.Add(evaluation);
                await _context.SaveChangesAsync(); // Lấy EvaluationId
            }

            foreach (var item in request.Evaluations)
            {
                var criteria = await _context.EvaluationCriteriaMaster
                    .FirstOrDefaultAsync(c => c.CriteriaId == item.CriteriaId);

                if (criteria == null) continue;

                if (!criteria.IsScoredByStudent) continue;

                var detail = await _context.EvaluationDetails
                    .FirstOrDefaultAsync(d => d.EvaluationId == evaluation.EvaluationId && d.CriteriaId == item.CriteriaId);

                if (detail == null)
                {
                    detail = new EvaluationDetails
                    {
                        EvaluationId = evaluation.EvaluationId,
                        CriteriaId = item.CriteriaId
                    };
                    _context.EvaluationDetails.Add(detail);
                }

                // Giới hạn điểm không vượt quá MaxScore
                detail.StudentScore = Math.Min(item.StudentScore ?? 0, criteria.MaxScore);

                // Lưu ghi chú nếu có
                if (!string.IsNullOrWhiteSpace(item.Note))
                {
                    var note = await _context.EvaluationNotes
                        .FirstOrDefaultAsync(n => n.EvaluationId == evaluation.EvaluationId && n.CriteriaId == item.CriteriaId);

                    if (note == null)
                    {
                        note = new EvaluationNotes
                        {
                            EvaluationId = evaluation.EvaluationId,
                            CriteriaId = item.CriteriaId,
                            EvaluatedBy = request.StudentId,
                            EvaluatedByRole = 3,
                            EvaluatedAt = DateTime.Now
                        };
                        _context.EvaluationNotes.Add(note);
                    }

                    note.Note = item.Note;
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ConfirmEvaluationAsync(int studentId, int semesterId, int academicYearId)
        {
            if (semesterId <= 0 || academicYearId <= 0)
                return false;

            // Lấy Evaluation hiện tại
            var evaluation = await _context.Evaluations
                .FirstOrDefaultAsync(e => e.StudentId == studentId && e.SemesterId == semesterId);

            if (evaluation == null)
                return false;

            // Đánh dấu đã xác nhận
            evaluation.Status = "Submitted";
            evaluation.SubmittedAt = DateTime.Now;

            // Lấy CriteriaSetId tương ứng
            var criteriaSet = await _context.EvaluationCriteriaSets
                .FirstOrDefaultAsync(cs => cs.SemesterId == semesterId && cs.AcademicYearId == academicYearId);

            if (criteriaSet == null)
                return false;

            var criteriaIds = await _context.EvaluationCriteriaSetDetails
                .Where(d => d.SetId == criteriaSet.SetId)
                .Select(d => d.CriteriaId)
                .ToListAsync();

            var details = await _context.EvaluationDetails
                .Where(d => d.EvaluationId == evaluation.EvaluationId)
                .ToListAsync();

            foreach (var criteriaId in criteriaIds)
            {
                var criteria = await _context.EvaluationCriteriaMaster
                    .FirstOrDefaultAsync(c => c.CriteriaId == criteriaId);

                if (criteria == null) continue;

                var detail = details.FirstOrDefault(d => d.CriteriaId == criteriaId);
                if (detail == null)
                {
                    detail = new EvaluationDetails
                    {
                        EvaluationId = evaluation.EvaluationId,
                        CriteriaId = criteriaId
                    };
                    _context.EvaluationDetails.Add(detail);
                }

                // Copy điểm sang ClassLeaderScore nếu có StudentScore
                if (detail.StudentScore.HasValue)
                {
                    detail.ClassLeaderScore = detail.StudentScore;
                }

                // Ghi điểm từ hoạt động nếu IsUploadOnly = true
                if (criteria.IsUploadOnly)
                {
                    var activityScore = await (from ar in _context.ActivityRegistrations
                                               join a in _context.Activities on ar.ActivityId equals a.ActivityId
                                               where ar.StudentId == studentId
                                                  && ar.Attended == true
                                                  && a.ScoreForCriteria == criteriaId.ToString()
                                               select ar.ActualScore).FirstOrDefaultAsync();

                    if (activityScore.HasValue)
                    {
                        detail.StudentScore = activityScore;
                        detail.ClassLeaderScore = activityScore;
                    }
                }

                // Ghi điểm từ admin nếu IsScoredByAdmin = true
                if (criteria.IsScoredByAdmin)
                {
                    var adminEval = await _context.AdminEvaluationDetails
                        .FirstOrDefaultAsync(x => x.StudentId == studentId
                                                && x.CriteriaId == criteriaId
                                                && x.CriteriaSetId == criteriaSet.SetId);

                    if (adminEval != null)
                    {
                        detail.AdvisorScore = adminEval.Score;
                    }
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }

    }
}
