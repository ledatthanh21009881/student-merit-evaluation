using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PJ_XET_THI_DUA_KHEN_THUONG.Services;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using Microsoft.AspNetCore.Authorization;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/activity-registration")]
    public class ActivityRegistrationController : ControllerBase
{
    private readonly IActivityRegistrationService _service;

    public ActivityRegistrationController(IActivityRegistrationService service)
    {
        _service = service;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] ActivityRegistrationRequest request)
    {
        await _service.RegisterAsync(request);
        return Ok(new { message = "Đăng ký thành công" });
    }

    [HttpPost("unregister")]
    public async Task<IActionResult> Unregister([FromBody] ActivityRegistrationRequest request)
    {
        await _service.UnregisterAsync(request);
        return Ok(new { message = "Hủy đăng ký thành công" });
    }

    [HttpGet("is-registered")]
    public async Task<IActionResult> IsRegistered([FromQuery] int activityId, [FromQuery] int userId)
    {
        var request = new ActivityRegistrationRequest { ActivityId = activityId, UserId = userId };
        var result = await _service.IsRegisteredAsync(request);
        return Ok(new { isRegistered = result });
    }
}
}