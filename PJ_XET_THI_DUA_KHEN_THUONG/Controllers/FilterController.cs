using Microsoft.AspNetCore.Mvc;
using PJ_XET_THI_DUA_KHEN_THUONG.Data;
using Microsoft.EntityFrameworkCore;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilterController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FilterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/filter/semesters
        [HttpGet("semesters")]
        public async Task<IActionResult> GetSemesters()
        {
            var semesters = await _context.Semesters.ToListAsync();
            return Ok(semesters);
        }

        // GET: api/filter/academic-years
        [HttpGet("academic-years")]
        public async Task<IActionResult> GetAcademicYears()
        {
            var years = await _context.AcademicYears.ToListAsync();
            return Ok(years);
        }

        // GET: api/filter/academic-years/by-semester/1
        [HttpGet("academic-years/by-semester/{semesterId}")]
        public async Task<IActionResult> GetAcademicYearsBySemester(int semesterId)
        {
            var yearIds = await _context.Classes
                .Where(c => c.SemesterId == semesterId)
                .Select(c => c.AcademicYearId)
                .Distinct()
                .ToListAsync();

            var years = await _context.AcademicYears
                .Where(y => yearIds.Contains(y.AcademicYearId))
                .ToListAsync();

            return Ok(years);
        }

        // GET: api/filter/classes/by-year-semester?yearId=1&semesterId=2
        [HttpGet("classes/by-year-semester")]
        public async Task<IActionResult> GetClassesByYearAndSemester(int yearId, int semesterId)
        {
            var classes = await _context.Classes
                .Where(c => c.AcademicYearId == yearId && c.SemesterId == semesterId)
                .ToListAsync();

            return Ok(classes);
        }

        // GET: api/filter/faculties
        [HttpGet("faculties")]
        public async Task<IActionResult> GetFaculties()
        {
            var faculties = await _context.Faculties.ToListAsync();
            return Ok(faculties);
        }
    }
}
