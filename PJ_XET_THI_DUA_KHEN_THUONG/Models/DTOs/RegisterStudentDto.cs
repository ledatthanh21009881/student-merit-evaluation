namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs
{
    public class RegisterStudentDto
    {
        public string StudentCode { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;  // Thêm dòng này
        public string Password { get; set; } = string.Empty;
    }
}
