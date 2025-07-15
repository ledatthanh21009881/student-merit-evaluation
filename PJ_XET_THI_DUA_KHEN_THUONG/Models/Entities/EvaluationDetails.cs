using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("EvaluationDetails")]
    public class EvaluationDetails
    {
        [Key]
        public int EvaluationDetailID { get; set; }
        public int EvaluationID { get; set; }
        public int CriteriaID { get; set; }
        public int? StudentScore { get; set; }
        public int? ClassLeaderScore { get; set; }
        public int? AdvisorScore { get; set; }
        [MaxLength(255)]
        public string? Note { get; set; }

        // Navigation properties
        public virtual Evaluations Evaluation { get; set; }
        public virtual Criteria Criteria { get; set; }
    }
} 