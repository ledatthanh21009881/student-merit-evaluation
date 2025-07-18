using Microsoft.AspNetCore.Mvc;
using PJ_XET_THI_DUA_KHEN_THUONG.Services;
using PJ_XET_THI_DUA_KHEN_THUONG.Models.DTOs.request;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PJ_XET_THI_DUA_KHEN_THUONG.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/evaluation")]
    public class EvaluationController : ControllerBase
    {
        private readonly IEvaluationService _service;
        public EvaluationController(IEvaluationService service) { _service = service; }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("user/{userId}/form/{formId}/semester/{semester}")]
        public async Task<IActionResult> GetByUserFormSemester(int userId, int formId, string semester)
        {
            var result = await _service.GetByUserFormSemesterAsync(userId, formId, semester);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdate(EvaluationRequest request)
        {
            await _service.AddOrUpdateAsync(request);
            return Ok(new { message = "Evaluation saved successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("admin/filter")]
public async Task<IActionResult> AdminFilter([FromBody] EvaluationAdminFilterRequest request)
{
    var result = await _service.AdminFilterAsync(request);
    return Ok(result);
}
    }
} 