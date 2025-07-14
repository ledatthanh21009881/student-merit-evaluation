using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("CriteriaForms")]
    public class CriteriaForm
    {
        [Key]
        public int CriteriaFormID { get; set; }
        public string FormName { get; set; } = null!;
        public int AcademicYearStart { get; set; }
        public string Semester { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FormType { get; set; } = null!;
        public bool IsActive { get; set; }

        // Mối quan hệ 1-N với FormTimeline
        public virtual ICollection<FormTimeline> FormTimelines { get; set; } = new List<FormTimeline>();

        // Mối quan hệ N-N với CriteriaForm
        public virtual ICollection<Criteria> Criterias { get; set; } = new List<Criteria>();

    }
}
