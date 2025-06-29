using System.ComponentModel.DataAnnotations;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    public class Classes
    {
        [Key]
        public int ClassId { get; set; }

        public int AcademicYearId { get; set; }
        public int SemesterId { get; set; }

        public string ClassName { get; set; } = "";
        public int FacultyId { get; set; }
    }

}
