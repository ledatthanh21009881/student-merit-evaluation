using Microsoft.AspNetCore.Mvc;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using PJ_XET_THI_DUA_KHEN_THUONG.Services;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Controllers
{
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
    }
}