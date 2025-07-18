using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PJ_XET_THI_DUA_KHEN_THUONG.Services;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("students/evaluated")]
        public async Task<IActionResult> GetEvaluatedStudents()
        {
            try
            {
                var result = await _userService.GetEvaluatedStudentsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi truy xuất danh sách sinh viên đã đánh giá.", error = ex.Message });
            }
        }

        [HttpGet("students")]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                var result = await _userService.GetStudentsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi truy xuất danh sách sinh viên đã đánh giá.", error = ex.Message });
            }
        }

        [HttpGet("students/evaluated/filter")]
        public async Task<IActionResult> GetEvaluatedStudentsFilter(
            [FromQuery] string? semester = null,
            [FromQuery] int? academicYear = null,
            [FromQuery] string? status = null)
        {
            try
            {
                var result = await _userService.GetEvaluatedStudentsFilterAsync(semester, academicYear, status);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi lọc danh sách sinh viên đã đánh giá.", error = ex.Message });
            }
        }

        [HttpGet("students/evaluated/{userId}")]
        public async Task<IActionResult> GetEvaluatedStudentDetails(int userId)
        {
            try
            {
                var result = await _userService.GetEvaluatedStudentDetailsAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi truy xuất chi tiết sinh viên.", error = ex.Message });
            }
        }

    }
}