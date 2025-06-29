namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs
{
    public class ActivityDto
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; } = string.Empty;
        public string ScoreForCriteria { get; set; } = string.Empty; // VD: "DRL01"
        public int AccumulatedScore { get; set; }
        public bool Attended { get; set; } // true nếu sinh viên đã tham gia
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CriteriaCode { get; internal set; }
    }
}
