namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs
{
    public class EvaluationConfirmDto
    {
        public int EvaluationId { get; set; }
        public decimal TotalScore { get; set; }
        public decimal Deduction { get; set; }
        public decimal FinalTotal { get; set; }
        public string DeductionNote { get; set; } = string.Empty;
    }

}
