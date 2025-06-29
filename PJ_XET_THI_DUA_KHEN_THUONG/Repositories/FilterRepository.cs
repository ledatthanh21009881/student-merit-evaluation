using Microsoft.EntityFrameworkCore;
using PJ_XET_THI_DUA_KHEN_THUONG.Data;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories
{
    public class FilterRepository : IFilterRepository
    {
        private readonly ApplicationDbContext _context;

        public FilterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Semesters>> GetSemestersAsync()
        {
            return await _context.Semesters.ToListAsync();
        }

        public async Task<List<AcademicYears>> GetAcademicYearsAsync()
        {
            return await _context.AcademicYears.ToListAsync();
        }

        public async Task<List<Faculties>> GetFacultiesAsync()
        {
            return await _context.Faculties.ToListAsync();
        }

        public async Task<List<Classes>> GetClassesByFacultyIdAsync(int facultyId)
        {
            return await _context.Classes.Where(c => c.FacultyId == facultyId).ToListAsync();
        }

        public async Task<List<Classes>> GetClassesByAcademicYearAndSemesterAsync(int yearId, int semesterId)
        {
            return await _context.Classes.Where(c => c.AcademicYearId == yearId && c.SemesterId == semesterId).ToListAsync();
        }
    }
}
