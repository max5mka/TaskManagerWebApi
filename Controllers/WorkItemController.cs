using Microsoft.AspNetCore.Mvc;
using TaskManagerWebApi.Models.Services;

namespace TaskManagerWebApi.Controllers
{
    [ApiController]
    [Route("WorkItems")]
    public class WorkItemController(IWorkItemService _workItemService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            [FromQuery] string? status = null,
            [FromQuery] string? priority = null,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var list = await _workItemService.GetAllAsync(status, priority, page, pageSize);
            return Ok(list);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdASync([FromRoute] int id)
        {
            var result = await _workItemService.GetByIdAsync(id);
            return Ok(result);
        }
    }
}
