using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("Evaluations")]
    public class Evaluations
    {
        [Key]
        public int EvaluationId { get; set; }
        public int StudentId { get; set; }
        public int SemesterId { get; set; }
        public string? Status { get; set; }
        public DateTime? SubmittedAt { get; set; }
    }
}
