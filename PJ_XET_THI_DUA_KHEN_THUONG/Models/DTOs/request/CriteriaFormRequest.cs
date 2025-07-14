using System.ComponentModel.DataAnnotations;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request
{
    public class CriteriaFormRequest
    {
        [Required(ErrorMessage = "Tên form là bắt buộc")]
        [MaxLength(255, ErrorMessage = "Tên form không được vượt quá 255 ký tự")]
        public string FormName { get; set; } = null!;

        [Required(ErrorMessage = "Năm học bắt đầu là bắt buộc")]
        [Range(2020, 3000, ErrorMessage = "Năm học phải từ 2020 trở đi")]
        public int AcademicYearStart { get; set; }

        [Required(ErrorMessage = "Học kỳ là bắt buộc")]
        public string Semester { get; set; } = null!;

        public string? Description { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu là bắt buộc")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc là bắt buộc")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Loại form là bắt buộc")]
        public string FormType { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        // Danh sách ID các tiêu chí được chọn
        public List<int> SelectedCriteriaIds { get; set; } = new List<int>();

        // Danh sách timeline
        public List<FormTimelineRequest> Timelines { get; set; } = new List<FormTimelineRequest>();
    }

}