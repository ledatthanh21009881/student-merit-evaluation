using Microsoft.AspNetCore.Mvc;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs;
using PJ_XET_THI_DUA_KHEN_THUONG.Services;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Controllers
{
    [Route("api/evaluation")]
    [ApiController]
    public class StudentEvaluationController : ControllerBase
    {
        private readonly IStudentEvaluationService _evaluationService;

        public StudentEvaluationController(IStudentEvaluationService evaluationService)
        {
            _evaluationService = evaluationService;
        }

        /// <summary>
        /// API lấy danh sách tiêu chí đánh giá (gồm điểm, ghi chú, minh chứng)
        /// </summary>
        [HttpGet("form")]
        public async Task<IActionResult> GetEvaluationForm([FromQuery] int studentId, [FromQuery] int semesterId, [FromQuery] int academicYearId)
        {
            var result = await _evaluationService.GetEvaluationFormAsync(studentId, semesterId, academicYearId);
            return Ok(result);
        }

        /// <summary>
        /// API lưu điểm + ghi chú đánh giá của sinh viên
        /// </summary>
        [HttpPost("save")]
        public async Task<IActionResult> SaveEvaluation([FromBody] SaveEvaluationRequestDto request)
        {
            var success = await _evaluationService.SaveEvaluationAsync(request);
            if (success)
                return Ok(new { message = "Đã lưu đánh giá thành công." });
            return BadRequest(new { message = "Lưu đánh giá thất bại." });
        }

        /// <summary>
        /// API xác nhận hoàn tất đánh giá
        /// </summary>
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmEvaluation([FromQuery] int studentId, [FromQuery] int semesterId, [FromQuery] int academicYearId)
        {
            var success = await _evaluationService.ConfirmEvaluationAsync(studentId, semesterId, academicYearId);
            if (success)
                return Ok(new { message = "Đã xác nhận đánh giá." });
            return BadRequest(new { message = "Xác nhận thất bại." });
        }
    }
}
