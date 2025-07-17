using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;
using PJ_XET_THI_DUA_KHEN_THUONG.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PJ_XET_THI_DUA_KHEN_THUONG.Data;
using Microsoft.EntityFrameworkCore;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services.Implement
{
    public class EvaluationService : IEvaluationService
    {
        private readonly IEvaluationRepository _repo;
        private readonly ApplicationDbContext _context;
        public EvaluationService(IEvaluationRepository repo, ApplicationDbContext context) { _repo = repo; _context = context; }

        public async Task<List<EvaluationResponse>> GetAllAsync()
        {
            var list = await _repo.GetAllAsync();
            return list.Select(MapToResponse).ToList();
        }

        public async Task<EvaluationResponse?> GetByIdAsync(int id)
        {
            var e = await _repo.GetByIdAsync(id);
            return e == null ? null : MapToResponse(e);
        }

        public async Task<EvaluationResponse?> GetByUserFormSemesterAsync(int userId, int formId, string semester)
        {
            var e = await _repo.GetByUserFormSemesterAsync(userId, formId, semester);
            return e == null ? null : MapToResponse(e);
        }

        public async Task AddOrUpdateAsync(EvaluationRequest request)
        {
            // Tìm evaluation tổng
            var eval = await _repo.GetByUserFormSemesterAsync(request.UserID, request.CriteriaFormID, request.Semester);
            if (eval == null)
            {
                eval = new Evaluations
                {
                    UserID = request.UserID,
                    CriteriaFormID = request.CriteriaFormID,
                    Semester = request.Semester,
                    Note = request.Note,
                    Status = request.Status,
                    Classification = request.Classification,
                    Details = new List<EvaluationDetails>()
                };
                await _repo.AddAsync(eval);
            }
            // Cập nhật chi tiết
            foreach (var detail in request.Details)
            {
                var exist = eval.Details.FirstOrDefault(d => d.CriteriaID == detail.CriteriaID);
                if (exist == null)
                {
                    eval.Details.Add(new EvaluationDetails
                    {
                        CriteriaID = detail.CriteriaID,
                        StudentScore = detail.StudentScore,
                        ClassLeaderScore = detail.ClassLeaderScore,
                        AdvisorScore = detail.AdvisorScore,
                        Note = detail.Note
                    });
                }
                else
                {
                    exist.StudentScore = detail.StudentScore;
                    exist.ClassLeaderScore = detail.ClassLeaderScore;
                    exist.AdvisorScore = detail.AdvisorScore;
                    exist.Note = detail.Note;
                }
            }
            // Tính tổng điểm (ví dụ: tổng điểm sinh viên)
            eval.TotalScore = eval.Details.Sum(d => d.StudentScore ?? 0);
            await _repo.UpdateAsync(eval);
        }

        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);

        private EvaluationResponse MapToResponse(Evaluations e)
        {
            return new EvaluationResponse
            {
                EvaluationsID = e.EvaluationsID,
                UserID = e.UserID,
                CriteriaFormID = e.CriteriaFormID,
                Semester = e.Semester,
                TotalScore = e.TotalScore,
                Note = e.Note,
                Status = e.Status,
                SubmittedAt = e.SubmittedAt,
                LockedBy = e.LockedBy,
                LockedAt = e.LockedAt,
                Classification = e.Classification,
                Details = e.Details.Select(d => new EvaluationDetailResponse
                {
                    EvaluationDetailID = d.EvaluationDetailID,
                    CriteriaID = d.CriteriaID,
                    StudentScore = d.StudentScore,
                    ClassLeaderScore = d.ClassLeaderScore,
                    AdvisorScore = d.AdvisorScore,
                    Note = d.Note
                }).ToList()
            };
        }

        public async Task<List<EvaluationAdminFilterResponse>> AdminFilterAsync(EvaluationAdminFilterRequest request)
        {
            var query = from e in _repo.GetQueryable()
                        join u in _context.Users on e.UserID equals u.UserID
                        join f in _context.CriteriaForms on e.CriteriaFormID equals f.CriteriaFormID
                        where f.AcademicYearStart == request.AcademicYearStart
                           && e.Semester.ToString().Contains(request.Semester)
                        select new EvaluationAdminFilterResponse
                        {
                            UserID = u.UserID,
                            LastName = u.LastName,
                            FirstName = u.FirstName,
                            CriteriaFormID = f.CriteriaFormID,
                            FormName = f.FormName,
                            AcademicYearStart = f.AcademicYearStart,
                            Semester = e.Semester.ToString(),
                            EvaluationID = e.EvaluationsID,
                            Status = e.Status,
                            Classification = e.Classification
                        };
            return await query.ToListAsync();
        }
    }
} 