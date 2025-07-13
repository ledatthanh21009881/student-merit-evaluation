using System.ComponentModel.DataAnnotations;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request
{
    public class FormTimelineRequest
    {
        [Required(ErrorMessage = "Tên bước là bắt buộc")]
        public string StepName { get; set; } = null!;

        [Required(ErrorMessage = "Đối tượng thực hiện là bắt buộc")]
        public string RoleTarget { get; set; } = null!;

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Description { get; set; }
    }
}
