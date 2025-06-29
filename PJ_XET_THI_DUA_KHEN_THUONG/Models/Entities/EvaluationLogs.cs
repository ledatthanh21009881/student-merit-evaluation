using System.ComponentModel.DataAnnotations;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    public class EvaluationLogs
    {
        [Key]
        public int LogId { get; set; }
        public int EvaluationDetailId { get; set; }
        public string ActionBy { get; set; } = string.Empty;
        public string ActionNote { get; set; } = string.Empty;
        public DateTime? ActionDate { get; set; }
    }
}
