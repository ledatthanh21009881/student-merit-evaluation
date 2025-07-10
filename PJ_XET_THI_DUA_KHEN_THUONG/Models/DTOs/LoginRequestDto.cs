/**
* File: LoginRequestDto.cs
* Description: DTO dùng để nhận thông tin đăng nhập từ người dùng (username, password).
* Created by: Thành
* Created on: 2025-07-09
*/

namespace PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs
{
    public class LoginRequestDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
