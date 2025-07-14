using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using PJ_XET_THI_DUA_KHEN_THUONG.Services;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/activity")]
    public class ActivityController : ControllerBase
    {
        private readonly IActivitiesService _service;

        public ActivityController(IActivitiesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllActivitiesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetActivityByID(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ActivityRequest request)
        {
            await _service.CreateAsync(request);
            return Ok(new { message = "Activity created successfully." });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ActivityRequest request)
        {
            try
            {
                await _service.UpdateAsync(id, request);
                return Ok(new { message = "Activity updated successfully." });
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
