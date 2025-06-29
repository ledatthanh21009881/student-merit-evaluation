using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;
using PJ_XET_THI_DUA_KHEN_THUONG.Repositories;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Services
{
    public class FilterService : IFilterService
    {
        private readonly IFilterRepository _filterRepository;

        public FilterService(IFilterRepository filterRepository)
        {
            _filterRepository = filterRepository;
        }

        public Task<List<Semesters>> GetSemestersAsync() => _filterRepository.GetSemestersAsync();

        public Task<List<AcademicYears>> GetAcademicYearsAsync() => _filterRepository.GetAcademicYearsAsync();

        public Task<List<Faculties>> GetFacultiesAsync() => _filterRepository.GetFacultiesAsync();

        public Task<List<Classes>> GetClassesByFacultyIdAsync(int facultyId) => _filterRepository.GetClassesByFacultyIdAsync(facultyId);

        public Task<List<Classes>> GetClassesByAcademicYearAndSemesterAsync(int yearId, int semesterId) => _filterRepository.GetClassesByAcademicYearAndSemesterAsync(yearId, semesterId);
    }
}
