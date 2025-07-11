using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("ActivityCategories")]
    public class ActivityCategory
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        [MaxLength(255)]
        public string CategoryName { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }

}
