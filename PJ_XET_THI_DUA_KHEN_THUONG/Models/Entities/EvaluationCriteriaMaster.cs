using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("EvaluationCriteriaMaster")]
    public class EvaluationCriteriaMaster
    {
        [Key]
        public int CriteriaId { get; set; }
        public int GroupId { get; set; }
        public string? CriteriaName { get; set; }
        public string? Description { get; set; }
        public int MaxScore { get; set; }
        public bool IsScoredByStudent { get; set; }
        public bool IsScoredByAdmin { get; set; }
        public bool IsUploadOnly { get; set; }
    }

}
