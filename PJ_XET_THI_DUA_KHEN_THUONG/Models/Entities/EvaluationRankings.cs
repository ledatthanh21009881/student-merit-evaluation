using System.ComponentModel.DataAnnotations;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    public class EvaluationRankings
    {
        [Key]
        public int RankingId { get; set; }
        public int CriteriaSetId { get; set; }
        public int StudentId { get; set; }
        public int FinalScore { get; set; }
        public string Classification { get; set; } = string.Empty;
    }
}
