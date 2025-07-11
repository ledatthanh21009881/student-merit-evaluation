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
        /// Đăng nhập dành cho Admin, Khoa, Cố vấn, Lớp trưởng (bằng Username)
        /// </summary>
        [HttpPost("login-admin")]
        public async Task<IActionResult> LoginAdmin([FromBody] LoginRequestDto request)
        {
            var result = await _authService.LoginAdminAsync(request.Username, request.Password);
            if (result == null)
                return Unauthorized(new { message = "Sai tài khoản hoặc mật khẩu" });

            return Ok(result);
        }

        /// <summary>
        /// Đăng nhập dành cho Sinh viên (bằng MSSV = IdentityCode)
        /// </summary>
        [HttpPost("login-student")]
        public async Task<IActionResult> LoginStudent([FromBody] LoginRequestDto request)
        {
            var result = await _authService.LoginStudentAsync(request.Username, request.Password);
            if (result == null)
                return Unauthorized(new { message = "Mã sinh viên hoặc mật khẩu không đúng" });

            return Ok(result);
        }
    }
}
