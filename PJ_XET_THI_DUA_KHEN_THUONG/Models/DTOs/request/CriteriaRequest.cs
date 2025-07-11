using System.ComponentModel.DataAnnotations;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request
{
    public class CriteriaRequest
    {
        public string CriteriaName { get; set; }
        public string? Description { get; set; }
        [Range(0, 100)]
        public int MaxScore { get; set; }
        [Range(1, 5)]
        public int Level { get; set; }
        public bool IsStudentScored { get; set; }
        public bool IsAdminScored { get; set; }
        public bool IsUploadOnly { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public int CriteriaTypeID { get; set; }
        public int? ParentID { get; set; }
    }
}
