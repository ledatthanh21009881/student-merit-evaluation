using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("Roles")]
    public class Roles
    {
        [Key]
        public int RoleID { get; set; }

        public string RoleName { get; set; } = null!;

        // Navigation
        public ICollection<Accounts> Accounts { get; set; } = new List<Accounts>();
    }
}
