namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response
{

    public class FormTimelineResponse
    {
        public int TimelineId { get; set; }
        public string StepName { get; set; } = null!;
        public string RoleTarget { get; set; } = null!;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
    }
}
