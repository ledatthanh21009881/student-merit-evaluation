namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs
{
    public class EvaluationDto
    {
        public int CriteriaId { get; set; }
        public string? CriteriaName { get; set; }
        public string? Description { get; set; }
        public int MaxScore { get; set; }
        public bool IsScoredByStudent { get; set; }
        public bool IsUploadOnly { get; set; }

        public int? StudentScore { get; set; }
        public int? AdminScore { get; set; }
        public string? StudentNote { get; set; }
        public string? AdminNote { get; set; }
        public string? EvidencePath { get; set; }
    }
}
