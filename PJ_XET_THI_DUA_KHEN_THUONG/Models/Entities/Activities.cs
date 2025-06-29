using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("Activities")]
    public class Activities
    {
        [Key]
        public int ActivityId { get; set; }
        public string? ActivityName { get; set; }
        public string? Description { get; set; }

        public int AccumulatedScore { get; set; }
        public int Quantity { get; set; }

        public int SemesterId { get; set; }
        public int AcademicYearId { get; set; }
        public int UnitId { get; set; }

        public DateTime? RegistrationStart { get; set; }
        public DateTime? RegistrationEnd { get; set; }
        public DateTime? AttendanceStart { get; set; }
        public DateTime? AttendanceEnd { get; set; }

        public bool ShowInApp { get; set; }
        public bool AllowEarlyRegistration { get; set; }

        public int CreatedBy { get; set; }

        public string? ScoreForCriteria { get; set; }
    }
}
