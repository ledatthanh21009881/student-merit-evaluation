using Microsoft.AspNetCore.Mvc;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using PJ_XET_THI_DUA_KHEN_THUONG.Services;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CriteriaController : ControllerBase
    {
        private readonly ICriteriaService _criteriaService;

        public CriteriaController(ICriteriaService criteriaService)
        {
            _criteriaService = criteriaService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCriteria()
        {
            try
            {
                var criteriaList = await _criteriaService.GetCriteriaTreeAsync();
                return Ok(criteriaList);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, new { message = "An error occurred while retrieving criteria.", error = ex.Message });
            }
        }

        [HttpGet("all-flat")]
        public async Task<IActionResult> GetAllCriteria_Flat()
        {
            try
            {
                var criteriaList = await _criteriaService.GetCriteriaFlatAsync();
                return Ok(criteriaList);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, new { message = "An error occurred while retrieving criteria.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCriteriaByID(int id)
        {
            try
            {
                var criteria = await _criteriaService.GetCriteriaByID(id);
                return Ok(criteria);
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here if needed
                return StatusCode(500, new { message = "An error occurred while retrieving criteria.", error = ex.Message });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CriteriaRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _criteriaService.CreateAsync(request);
            return Ok(new { message = "Thêm tiêu chí thành công." });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CriteriaRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _criteriaService.UpdateAsync(id, request);
            return Ok(new { message = "Cập nhật tiêu chí thành công." });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var resut = await _criteriaService.DeleteAsync(id);
            if (!resut)
            {
                return NotFound("Không xóa thành công");
            }
            return Ok(new { message = "Xóa tiêu chí thành công." });
        }

        [HttpGet("by-type")]
        public async Task<IActionResult> GetByCriteriaType([FromQuery] int criteriaTypeId)
        {
            try
            {
                var result = await _criteriaService.GetByCriteriaTypeAsync(criteriaTypeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving criteria by type.", error = ex.Message });
            }
        }
    }
}
