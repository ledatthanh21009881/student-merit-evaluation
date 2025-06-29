using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("EvaluationCriteriaSets")]
    public class EvaluationCriteriaSets
    {
        [Key]
        public int SetId { get; set; }
        public int SemesterId { get; set; }
        public int AcademicYearId { get; set; }
        public string? Description { get; set; }
    }
}
