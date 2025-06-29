namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs
{
    public class SaveEvaluationRequestDto
    {
        public int StudentId { get; set; }
        public int SemesterId { get; set; }
        public int AcademicYearId { get; set; }
        public List<EvaluationItemDto> Evaluations { get; set; } = new();
    }
}
