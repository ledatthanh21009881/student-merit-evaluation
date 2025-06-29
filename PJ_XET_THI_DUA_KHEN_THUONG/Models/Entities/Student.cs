using System.ComponentModel.DataAnnotations;
using DocumentFormat.OpenXml.Spreadsheet;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public int UserId { get; set; }
        public string StudentCode { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? Phone { get; set; }
        public int? ClassId { get; set; }
        public string? Avatar { get; set; }
        public User? User { get; set; }
        public int? FacultyId { get; internal set; }
    }

}
