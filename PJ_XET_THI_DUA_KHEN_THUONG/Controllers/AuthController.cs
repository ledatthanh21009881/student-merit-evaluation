/**
 * File: AuthController.cs
 * Description: API xử lý đăng nhập cho sinh viên và tài khoản hệ thống (admin, khoa, cố vấn...).
 * Created by: Thành
 * Created on: 2025-07-10
 */
using Microsoft.AspNetCore.Mvc;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.Services.Interfaces;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Đăng nhập duy nhất cho tất cả vai trò: admin, khoa, cố vấn, sinh viên, lớp trưởng
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _authService.LoginAsync(request.Username, request.Password);
            if (result == null)
                return Unauthorized(new { message = "Sai thông tin đăng nhập" });

            return Ok(result);
        }
    }
}