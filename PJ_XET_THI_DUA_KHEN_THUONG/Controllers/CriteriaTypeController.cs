using Microsoft.AspNetCore.Mvc;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using PJ_XET_THI_DUA_KHEN_THUONG.Services;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CriteriaTypeController : ControllerBase
    {
        private readonly ICriteriaTypeService _criteriaTypeService;

        public CriteriaTypeController(ICriteriaTypeService criteriaTypeService)
        {
            _criteriaTypeService = criteriaTypeService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _criteriaTypeService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi truy xuất loại tiêu chí", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var result = await _criteriaTypeService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Đã xảy ra lỗi khi truy xuất loại tiêu chí", error = ex.Message });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CriteriaTypeRequest request)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            try
            {
                await _criteriaTypeService.CreateAsync(request);
                return Ok(new { message = "Thêm loại tiêu chí thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CriteriaTypeRequest request)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            try
            {
                await _criteriaTypeService.UpdateAsync(id, request);
                return Ok(new { message = "Cập nhật loại tiêu chí thành công." });
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
                var result = await _criteriaTypeService.DeleteAsync(id);
                if (!result)
                    return NotFound("Không tìm thấy loại tiêu chí.");

                return Ok(new { message = "Xóa loại tiêu chí thành công." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}