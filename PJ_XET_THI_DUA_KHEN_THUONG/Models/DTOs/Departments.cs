using System.ComponentModel.DataAnnotations;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.Entities
{
    public class Departments
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public int FacultyId { get; set; }
    }
}
