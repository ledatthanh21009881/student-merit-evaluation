using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("Criteria")]
    public class Criteria
    {
        [Key]
        public int CriteriaID { get; set; }

        public int Level { get; set; }

        [Required]
        [MaxLength(255)]
        public string CriteriaName { get; set; }

        public string? Description { get; set; }

        public int MaxScore { get; set; }

        public int CriteriaTypeID { get; set; }

        public bool IsStudentScored { get; set; }

        public bool IsAdminScored { get; set; }

        public bool IsUploadOnly { get; set; }

        public bool IsActive { get; set; }

        // FK đến chính bảng Criteria (Parent - con)
        public int? ParentID { get; set; }

        // Navigation properties
        [ForeignKey("CriteriaTypeID")]
        public virtual CriteriaType CriteriaType { get; set; } = null!;

        [ForeignKey("ParentID")]
        public virtual Criteria? ParentCriteria { get; set; } = null;

        public virtual ICollection<Criteria> SubCriteria { get; set; } = new List<Criteria>();

        // Mối quan hệ N-N với CriteriaForm
        public virtual ICollection<CriteriaForm> CriteriaForms { get; set; } = new List<CriteriaForm>();


        // Mối quan hệ nhiều nhiều với Activities thông qua ActivityCriteria
        public ICollection<ActivityCriteria> ActivityCriterias { get; set; } = new List<ActivityCriteria>();

        public override string? ToString()
        {
            return base.ToString();
        }
    }

}
