//using Microsoft.AspNetCore.Mvc;
//using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs;
//using PJ_XET_THI_DUA_KHEN_THUONG.Services;

//namespace PJ_XET_THI_DUA_KHEN_THUONG.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class EvaluationController : ControllerBase
//    {
//        private readonly IEvaluationService _service;

//        public EvaluationController(IEvaluationService service)
//        {
//            _service = service;
//        }

//        /// <summary>
//        /// Lấy danh sách tiêu chí + điểm đã đánh giá của sinh viên theo học kỳ + năm học
//        /// </summary>
//        [HttpGet("{studentId}/semester/{semesterId}/year/{yearId}")]
//        public async Task<IActionResult> GetEvaluations(int studentId, int semesterId, int yearId)
//        {
//            var result = await _service.GetEvaluationsByStudentAsync(studentId, semesterId, yearId);
//            return Ok(result);
//        }

//        /// <summary>
//        /// Lưu điểm rèn luyện sinh viên đánh giá
//        /// </summary>
//        [HttpPost("save")]
//        public async Task<IActionResult> SaveEvaluations(
//            [FromQuery] int studentId,
//            [FromQuery] int semesterId,
//            [FromQuery] int yearId,
//            [FromBody] List<EvaluationInputDto> evaluations)
//        {
//            var success = await _service.SaveStudentEvaluationAsync(studentId, semesterId, yearId, evaluations);
//            if (success)
//                return Ok(new { message = "Đã lưu thành công" });
//            else
//                return BadRequest(new { message = "Lưu thất bại" });
//        }

//        /// <summary>
//        /// Xác nhận hoàn tất đánh giá từ sinh viên (kết thúc đánh giá)
//        /// </summary>
//        [HttpPost("confirm")]
//        public async Task<IActionResult> ConfirmEvaluations(
//            [FromQuery] int studentId,
//            [FromQuery] int semesterId,
//            [FromQuery] int yearId)
//        {
//            var success = await _service.ConfirmStudentEvaluationAsync(studentId, semesterId, yearId);
//            if (success)
//                return Ok(new { message = "Đã xác nhận đánh giá" });
//            else
//                return BadRequest(new { message = "Xác nhận thất bại" });
//        }
//    }
//}
