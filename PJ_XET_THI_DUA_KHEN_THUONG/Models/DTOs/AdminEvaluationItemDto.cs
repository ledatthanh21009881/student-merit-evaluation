namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs
{
    public class AdminEvaluationItemDto
    {
        public int CriteriaId { get; set; }
        public string CriteriaName { get; set; } = string.Empty;
        public int Score { get; set; }
        public int MaxScore { get; set; }
        public string? Note { get; set; }
    }
}
