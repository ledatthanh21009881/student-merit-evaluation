namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs
{
    public class LoginResponseDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public string Token { get; set; } = string.Empty;
        public string? Avatar { get; set; }
        public bool IsClassLeader { get; set; }
        public int? ClassId { get; set; }
        public int? FacultyId { get; set; }
    }

}
