namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request
{
    public class ActivityRequest
    {
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

        // Mã tiêu chí liên kết
        public List<int> CriteriaIDs { get; set; }
    }

}