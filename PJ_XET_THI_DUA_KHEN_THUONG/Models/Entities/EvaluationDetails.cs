using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("EvaluationDetails")]
    public class EvaluationDetails
    {
        [Key]
        public int DetailId { get; set; }
        public int EvaluationId { get; set; }
        public int CriteriaId { get; set; }
        public int? StudentScore { get; set; }
        public int? ClassLeaderScore { get; set; }
        public int? AdvisorScore { get; set; }
    }
}
