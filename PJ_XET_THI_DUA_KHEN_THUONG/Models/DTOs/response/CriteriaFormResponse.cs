namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response
{
    public class CriteriaFormResponse
    {
        public int CriteriaFormID { get; set; }
        public string FormName { get; set; } = null!;
        public int AcademicYearStart { get; set; }
        public string Semester { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FormType { get; set; } = null!;
        public bool IsActive { get; set; }

        // Danh sách tiêu chí đã chọn
        public List<CriteriaResponse> SelectedCriteria { get; set; } = new List<CriteriaResponse>();

        // Danh sách timeline
        public List<FormTimelineResponse> Timelines { get; set; } = new List<FormTimelineResponse>();
    }
}