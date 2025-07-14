namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response
{
    public class ActivityResponse
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string? Description { get; set; }
        public int AccumulatedScore { get; set; }
        public int Quantity { get; set; }
        public string Semester { get; set; }
        public int AcademicYearStart { get; set; }
        public DateTime? RegistrationStart { get; set; }
        public DateTime? RegistrationEnd { get; set; }
        public DateTime? AttendanceStart { get; set; }
        public DateTime? AttendanceEnd { get; set; }
        public bool ShowInApp { get; set; }
        public bool AllowEarlyRegistration { get; set; }
        public bool IsActive { get; set; }

        // Danh sách tiêu chí liên kết
        public List<string> CriteriaNames { get; set; }
    }
}
