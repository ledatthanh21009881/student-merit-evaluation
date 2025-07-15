namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response
{
    public class EvaluationDetailResponse
    {
        public int EvaluationDetailID { get; set; }
        public int CriteriaID { get; set; }
        public int? StudentScore { get; set; }
        public int? ClassLeaderScore { get; set; }
        public int? AdvisorScore { get; set; }
        public string? Note { get; set; }
    }
} 