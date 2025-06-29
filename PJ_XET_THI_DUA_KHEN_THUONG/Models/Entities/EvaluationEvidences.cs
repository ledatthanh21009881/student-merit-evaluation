using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("EvaluationEvidence")]
    public class EvaluationEvidences
    {
        [Key]
        public int EvidenceId { get; set; }
        public int EvaluationId { get; set; }
        public int CriteriaId { get; set; }
        public string? FilePath { get; set; }
        public DateTime? UploadedAt { get; set; }
    }

}
