using Microsoft.EntityFrameworkCore;
using PJ_XET_THI_DUA_KHEN_THUONG.Data;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services.Implement
{
    public class ActivitiesService : IActivitiesService
    {
        private readonly ApplicationDbContext _context;

        public ActivitiesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ActivityResponse>> GetAllActivitiesAsync()
        {
            var activities = await _context.Activities
                .Include(a => a.ActivityCriterias)
                    .ThenInclude(ac => ac.Criteria)
                .ToListAsync();

            return activities.Select(a => new ActivityResponse
            {
                ActivityId = a.ActivityId,
                ActivityName = a.ActivityName,
                Description = a.Description,
                AccumulatedScore = a.AccumulatedScore,
                Quantity = a.Quantity,
                Semester = a.Semester,
                AcademicYearStart = a.AcademicYearStart,
                RegistrationStart = a.RegistrationStart,
                RegistrationEnd = a.RegistrationEnd,
                AttendanceStart = a.AttendanceStart,
                AttendanceEnd = a.AttendanceEnd,
                ShowInApp = a.ShowInApp,
                AllowEarlyRegistration = a.AllowEarlyRegistration,
                IsActive = a.IsActive,
                CriteriaNames = a.ActivityCriterias.Select(ac => ac.Criteria.CriteriaName).ToList()
            }).ToList();
        }

        public async Task<ActivityResponse?> GetActivityByID(int id)
        {
            var a = await _context.Activities
                .Include(ac => ac.ActivityCriterias)
                    .ThenInclude(c => c.Criteria)
                .FirstOrDefaultAsync(x => x.ActivityId == id);

            if (a == null) return null;

            return new ActivityResponse
            {
                ActivityId = a.ActivityId,
                ActivityName = a.ActivityName,
                Description = a.Description,
                AccumulatedScore = a.AccumulatedScore,
                Quantity = a.Quantity,
                Semester = a.Semester,
                AcademicYearStart = a.AcademicYearStart,
                RegistrationStart = a.RegistrationStart,
                RegistrationEnd = a.RegistrationEnd,
                AttendanceStart = a.AttendanceStart,
                AttendanceEnd = a.AttendanceEnd,
                ShowInApp = a.ShowInApp,
                AllowEarlyRegistration = a.AllowEarlyRegistration,
                IsActive = a.IsActive,
                CriteriaNames = a.ActivityCriterias.Select(c => c.Criteria.CriteriaName).ToList()
            };
        }

        public async Task CreateAsync(ActivityRequest request)
        {
            var entity = new Activites
            {
                ActivityName = request.ActivityName,
                Description = request.Description,
                AccumulatedScore = request.AccumulatedScore,
                Quantity = request.Quantity,
                Semester = request.Semester,
                AcademicYearStart = request.AcademicYearStart,
                RegistrationStart = request.RegistrationStart,
                RegistrationEnd = request.RegistrationEnd,
                AttendanceStart = request.AttendanceStart,
                AttendanceEnd = request.AttendanceEnd,
                ShowInApp = request.ShowInApp,
                AllowEarlyRegistration = request.AllowEarlyRegistration,
                IsActive = request.IsActive
            };

            _context.Activities.Add(entity);
            await _context.SaveChangesAsync();

            // Sau khi có ID mới, thêm ActivityCriteria
            if (request.CriteriaIDs?.Any() == true)
            {
                foreach (var criteriaId in request.CriteriaIDs)
                {
                    _context.ActivityCriteria.Add(new ActivityCriteria
                    {
                        ActivityId = entity.ActivityId,
                        CriteriaID = criteriaId
                    });
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(int idActivities, ActivityRequest request)
        {
            var existing = await _context.Activities
                .Include(a => a.ActivityCriterias)
                .FirstOrDefaultAsync(a => a.ActivityId == idActivities);

            if (existing == null)
                throw new Exception("Activity not found");

            existing.ActivityName = request.ActivityName;
            existing.Description = request.Description;
            existing.AccumulatedScore = request.AccumulatedScore;
            existing.Quantity = request.Quantity;
            existing.Semester = request.Semester;
            existing.AcademicYearStart = request.AcademicYearStart;
            existing.RegistrationStart = request.RegistrationStart;
            existing.RegistrationEnd = request.RegistrationEnd;
            existing.AttendanceStart = request.AttendanceStart;
            existing.AttendanceEnd = request.AttendanceEnd;
            existing.ShowInApp = request.ShowInApp;
            existing.AllowEarlyRegistration = request.AllowEarlyRegistration;
            existing.IsActive = request.IsActive;

            // Cập nhật lại các tiêu chí
            var oldCriteria = _context.ActivityCriteria.Where(ac => ac.ActivityId == idActivities);
            _context.ActivityCriteria.RemoveRange(oldCriteria);

            if (request.CriteriaIDs?.Any() == true)
            {
                foreach (var criteriaId in request.CriteriaIDs)
                {
                    _context.ActivityCriteria.Add(new ActivityCriteria
                    {
                        ActivityId = idActivities,
                        CriteriaID = criteriaId
                    });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Activities
                .Include(a => a.ActivityCriterias)
                .FirstOrDefaultAsync(a => a.ActivityId == id);

            if (existing == null) return false;

            _context.ActivityCriteria.RemoveRange(existing.ActivityCriterias);
            _context.Activities.Remove(existing);

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
