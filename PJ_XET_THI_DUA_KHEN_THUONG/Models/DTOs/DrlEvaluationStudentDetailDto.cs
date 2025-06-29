namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs
{
    public class DrlEvaluationStudentDetailDto
    {
        public int CriteriaId { get; set; }
        public string CriteriaName { get; set; } = "";
        public int MaxScore { get; set; }
        public int StudentScore { get; set; }
        public bool IsActivityScore { get; set; } = false;
        public int ActivityTotalScore { get; set; } = 0;
    }

}
