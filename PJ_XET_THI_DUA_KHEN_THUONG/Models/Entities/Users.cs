using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        public string LastName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string IdentityCode { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? Avatar { get; set; }
        public string? TrainingType { get; set; }
        public string? EducationLevel { get; set; }
        public string? AcademicYear { get; set; }
        public DateTime? JoinDate { get; set; }

        // Foreign keys
        public int? ClassID { get; set; }
        public int? FacultyID { get; set; }
        public int? MajorId { get; set; }

        // Navigation
        public ICollection<Accounts> Accounts { get; set; } = new List<Accounts>();
        public virtual ICollection<ActivityRegistration> ActivityRegistrations { get; set; }
    }
}
