using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("AdminEvaluationDetails")]
    public class AdminEvaluationDetails
    {
        [Key]
        public int AdminEvaluationId { get; set; }

        public int StudentId { get; set; }
        public int CriteriaSetId { get; set; }
        public int CriteriaId { get; set; }

        public int Score { get; set; }
        public string? Note { get; set; }

        public int EvaluatedBy { get; set; }
        public DateTime? EvaluatedAt { get; set; }
    }
}
