// Controllers/AuthController.cs
using Microsoft.AspNetCore.Mvc;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs;
using PJ_XET_THI_DUA_KHEN_THUONG.Services;

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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var result = await _authService.AuthenticateAsync(request);
            if (result == null)
                return Unauthorized("Thông tin đăng nhập không hợp lệ.");
            return Ok(result);
        }
    }
}
