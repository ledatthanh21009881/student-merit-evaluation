using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("Evaluations")]
    public class Evaluations
    {
        [Key]
        public int EvaluationsID { get; set; }
        public int UserID { get; set; }
        public int CriteriaFormID { get; set; }
        public int Semester { get; set; }
        public int? TotalScore { get; set; }
        [MaxLength(255)]
        public string? Note { get; set; }
        [MaxLength(50)]
        public string? Status { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public int? LockedBy { get; set; }
        public DateTime? LockedAt { get; set; }
        [MaxLength(50)]
        public string? Classification { get; set; }

        // Navigation properties
        public virtual Users User { get; set; }
        public virtual CriteriaForm CriteriaForm { get; set; }
        public virtual ICollection<EvaluationDetails> Details { get; set; } = new List<EvaluationDetails>();
    }
}
