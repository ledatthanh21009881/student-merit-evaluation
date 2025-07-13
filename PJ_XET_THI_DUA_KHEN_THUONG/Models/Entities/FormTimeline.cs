using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("FormTimelines")]
    public class FormTimeline
    {
        [Key]
        public int TimelineId { get; set; }              
        public int CriteriaFormID { get; set; }              

        public string StepName { get; set; } = null!;    
        public string RoleTarget { get; set; } = null!;      

        public DateTime? StartDate { get; set; }               
        public DateTime? EndDate { get; set; }               
        public string? Description { get; set; }

        //Mối quan hệ 1-n
        [ForeignKey("CriteriaFormID")]
        public virtual CriteriaForm CriteriaForm { get; set; } = null!;
    }
}
