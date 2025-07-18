using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using PJ_XET_THI_DUA_KHEN_THUONG.Services;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CriteriaFormController : ControllerBase
    {
        private readonly ICriteriaFormService _criteriaFormService;

        public CriteriaFormController(ICriteriaFormService criteriaFormService)
        {
            _criteriaFormService = criteriaFormService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _criteriaFormService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi truy xuất form tiêu chí.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _criteriaFormService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi truy xuất form tiêu chí.", error = ex.Message });
            }
        }

        

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CriteriaFormRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _criteriaFormService.CreateAsync(request);
                return Ok(new { message = "Tạo form tiêu chí thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CriteriaFormRequest request)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            try
            {
                await _criteriaFormService.UpdateAsync(id, request);
                return Ok(new { message = "Cập nhật form tiêu chí thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _criteriaFormService.DeleteAsync(id);
                if (!result)
                {
                    return NotFound("Không tìm thấy form tiêu chí.");

                }

                return Ok(new { message = "Xóa form tiêu chí thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetActiveForms()
        {
            try
            {
                var result = await _criteriaFormService.GetActiveFormsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi truy xuất biểu mẫu đang hoạt động.", error = ex.Message });
            }
        }

        [HttpGet("academic-year/{year}")]
        public async Task<IActionResult> GetByAcademicYear(int year)
        {
            try
            {
                var result = await _criteriaFormService.GetByAcademicYearAsync(year);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi truy xuất biểu mẫu theo năm học.", error = ex.Message });
            }
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetByFilter(
            [FromQuery] int? academicYear = null,
            [FromQuery] string? semester = null)
        {
            try
            {
                var result = await _criteriaFormService.GetByFilterAsync(academicYear, semester);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi lọc biểu mẫu.", error = ex.Message });
            }
        }

        [HttpGet("{id}/criteria")]
        public async Task<IActionResult> GetCriteriaByFormId(int id)
        {
            try
            {
                var result = await _criteriaFormService.GetCriteriaTreeByFormIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Lỗi khi lấy tiêu chí từ biểu mẫu.", error = ex.Message });
            }
        }

    }
}