// AdminEvaluationController.cs
using Microsoft.AspNetCore.Mvc;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs;
using PJ_XET_THI_DUA_KHEN_THUONG.Services;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminEvaluationController : ControllerBase
    {
        private readonly IAdminEvaluationService _service;

        public AdminEvaluationController(IAdminEvaluationService service)
        {
            _service = service;
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveAdminEvaluation([FromBody] AdminEvaluationDto dto)
        {
            var adminId = 1; // sau này sẽ lấy từ token
            await _service.SaveAdminEvaluationsAsync(dto, adminId);
            return Ok(new { message = "Lưu thành công" });
        }

        [HttpGet("{studentId}/{setId}")]
        public async Task<IActionResult> GetEvaluatedCriteria(int studentId, int setId)
        {
            var result = await _service.GetEvaluatedCriteriaAsync(studentId, setId);
            return Ok(result);
        }

        [HttpGet("criteria/{setId}")]
        public async Task<IActionResult> GetCriteriaForAdmin(int setId)
        {
            var result = await _service.GetCriteriaForAdminAsync(setId);
            return Ok(result);
        }
    }
}
