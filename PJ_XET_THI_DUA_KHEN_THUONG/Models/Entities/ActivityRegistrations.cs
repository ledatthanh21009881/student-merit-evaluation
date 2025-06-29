using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("ActivityRegistrations")]
    public class ActivityRegistrations
    {
        [Key]
        public int RegistrationId { get; set; }
        public int ActivityId { get; set; }
        public int StudentId { get; set; }
        public bool Attended { get; set; }
        public int? ActualScore { get; set; }
        public string? EvidencePath { get; set; }
    }
}
