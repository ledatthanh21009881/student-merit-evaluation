/**
 * File: LoginResponseDto.cs
 * Description: DTO trả về khi đăng nhập thành công, gồm token và thông tin người dùng.
 * Created by: Thành
 * Created on: 2025-07-09
 */
namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs
{
    public class LoginResponseDto
    {
        public string? Token { get; set; }
        public string? FullName { get; set; }
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        public int UserId { get; set; }
    }
}
