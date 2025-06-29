using Microsoft.AspNetCore.Mvc;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs;
using PJ_XET_THI_DUA_KHEN_THUONG.Services;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityCustomService _activityService;

        public ActivityController(IActivityCustomService activityService)
        {
            _activityService = activityService;
        }

        // Lấy hoạt động theo student + tiêu chí
        [HttpGet("student/{studentId}/criteria/{criteriaCode}")]
        public async Task<IActionResult> GetActivitiesByStudent(int studentId, string criteriaCode)
        {
            var result = await _activityService.GetActivitiesByStudentAndCriteriaAsync(studentId, criteriaCode);
            return Ok(result);
        }
    }
}
