using PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Repositories
{
    public interface IFilterRepository
    {
        Task<List<Semesters>> GetSemestersAsync();
        Task<List<AcademicYears>> GetAcademicYearsAsync();
        Task<List<Faculties>> GetFacultiesAsync();
        Task<List<Classes>> GetClassesByFacultyIdAsync(int facultyId);
        Task<List<Classes>> GetClassesByAcademicYearAndSemesterAsync(int yearId, int semesterId);
    }
}