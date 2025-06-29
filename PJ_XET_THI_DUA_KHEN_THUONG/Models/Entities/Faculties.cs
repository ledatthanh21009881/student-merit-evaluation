using System.ComponentModel.DataAnnotations;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    public class Faculties
    {
        [Key]
        public int FacultyId { get; set; }
        public string FacultyName { get; set; } = string.Empty;
    }
}
