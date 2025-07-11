
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("ActivityRegistrations")]
    public class ActivityRegistration
    {
        [Key, Column(Order = 0)]
        public int ActivityId { get; set; }

        [Key, Column(Order = 1)]
        public int UserID { get; set; }

        public DateTime RegisteredAt { get; set; }

        public bool Attended { get; set; }

        [MaxLength(255)]
        public string? EvidencePath { get; set; }

        [MaxLength(255)]
        public string? Note { get; set; }

        // Navigation properties
        public virtual Activites Activity { get; set; }
        public virtual Users User { get; set; }
    }
}
