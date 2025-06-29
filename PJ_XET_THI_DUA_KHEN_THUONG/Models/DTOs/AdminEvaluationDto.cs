namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs
{
    public class AdminEvaluationDto
    {
        public int StudentId { get; set; }
        public int CriteriaSetId { get; set; }
        public List<AdminEvaluationItemDto> Evaluations { get; set; }
    }
}
