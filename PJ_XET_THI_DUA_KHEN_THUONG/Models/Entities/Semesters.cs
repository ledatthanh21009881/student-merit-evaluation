using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("Semesters")]
    public class Semesters
    {
        [Key]
        public int SemesterId { get; set; }
        public string? SemesterName { get; set; }
        public int AcademicYearId { get; set; }

        [ForeignKey("AcademicYearId")]
        public AcademicYears? AcademicYears { get; set; }
    }
}
