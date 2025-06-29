namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs
{
    public class RegisterUserDto
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int RoleId { get; set; }
    }
}
