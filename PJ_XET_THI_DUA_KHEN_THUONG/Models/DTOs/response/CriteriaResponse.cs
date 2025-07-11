namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.response
{
    public class CriteriaResponse
    {
        public int CriteriaID { get; set; }
        public string CriteriaName { get; set; }
        public string? Description { get; set; }
        public int MaxScore { get; set; }
        public int Level { get; set; }

        public bool IsStudentScored { get; set; }
        public bool IsAdminScored { get; set; }
        public bool IsUploadOnly { get; set; }
        public bool IsActive { get; set; }

        public string? CriteriaTypeName { get; set; }
        public int? ParentID { get; set; }
        public string? ParentName { get; set; }
        public List<CriteriaResponse> Children { get; set; } = new();

    }
}
