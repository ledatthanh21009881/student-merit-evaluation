using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("EvaluationCriteriaSetDetails")]
    public class EvaluationCriteriaSetDetails
    {
        [Key]
        public int DetailId { get; set; }
        public int SetId { get; set; }
        public int CriteriaId { get; set; }

        [ForeignKey("CriteriaId")]
        public EvaluationCriteriaMaster? Criteria { get; set; }
    }
}
