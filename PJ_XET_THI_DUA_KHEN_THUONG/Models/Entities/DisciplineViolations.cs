using System.ComponentModel.DataAnnotations;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    public class DisciplineViolations
    {
        [Key]
        public int ViolationId { get; set; }
        public int StudentId { get; set; }
        public int CriteriaSetId { get; set; }
        public string Note { get; set; } = string.Empty;
        public int Deduction { get; set; }
    }
}
