using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("CriteriaTypes")]
    public class CriteriaType
    {
        [Key]
        public int CriteriaTypeID { get; set; }
        public string CriteriaTypeName { get; set; } = null!;

        public virtual ICollection<Criteria> CriteriaList { get; set; } = new List<Criteria>();

    }
}
