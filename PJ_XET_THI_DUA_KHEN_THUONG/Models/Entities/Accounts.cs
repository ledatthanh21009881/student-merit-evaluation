using DocumentFormat.OpenXml.Spreadsheet;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("Accounts")]
    public class Accounts
    {
        [Key]
        public int AccountID { get; set; }

        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public bool IsActive { get; set; }

        // Foreign Key + Navigation: Role
        public int RoleID { get; set; }
        [ForeignKey("RoleID")]
        public Roles Role { get; set; } = null!;

        // Foreign Key + Navigation: User
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public Users User { get; set; } = null!;
    }
}
