using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("AcademicYears")]
    public class AcademicYears
    {
        [Key]
        public int AcademicYearId { get; set; }
        public string? AcademicYearName { get; set; }
    }
}
