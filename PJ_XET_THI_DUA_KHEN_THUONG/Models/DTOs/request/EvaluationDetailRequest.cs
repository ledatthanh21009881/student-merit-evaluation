namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request
{
    public class EvaluationDetailRequest
    {
        public int CriteriaID { get; set; }
        public int? StudentScore { get; set; }
        public int? ClassLeaderScore { get; set; }
        public int? AdvisorScore { get; set; }
        public string? Note { get; set; }
    }
} 