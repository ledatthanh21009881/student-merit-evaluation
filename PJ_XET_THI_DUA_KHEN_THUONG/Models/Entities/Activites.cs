using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("Activities")]
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }

        [Required]
        [MaxLength(255)]
        public string ActivityName { get; set; }

        public string? Description { get; set; }

        public int CategoryID { get; set; }

        public int AccumulatedScore { get; set; }

        public int Quantity { get; set; }

        public string Semester { get; set; }

        public int AcademicYearStart { get; set; }

        public DateTime? RegistrationStart { get; set; }

        public DateTime? RegistrationEnd { get; set; }

        public DateTime? AttendanceStart { get; set; }

        public DateTime? AttendanceEnd { get; set; }

        public int CreatedBy { get; set; }

        public bool ShowInApp { get; set; }

        public bool AllowEarlyRegistration { get; set; }

        public bool IsActive { get; set; }

        // Là navigation property: giúp EF hiểu mối quan hệ và lấy dữ liệu liên kết giữa bảng.
        [ForeignKey("CategoryID")]
        public virtual ActivityCategory ActivityCategory { get; set; }

        // Navigation property cho mối quan hệ với ActivityRegistration
        public virtual ICollection<ActivityRegistration> ActivityRegistrations { get; set; }

    }

}
