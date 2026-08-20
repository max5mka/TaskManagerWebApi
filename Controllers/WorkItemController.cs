using Microsoft.AspNetCore.Mvc;
using TaskManagerWebApi.Models.DTOs;
using TaskManagerWebApi.Models.Services;

namespace TaskManagerWebApi.Controllers
{
    [ApiController]
    [Route("WorkItems")]
    public class WorkItemController(IWorkItemService _workItemService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] WorkItemFilterDTO filter)
        {
            var foundList = await _workItemService.GetAllAsync(filter);
            return Ok(foundList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdASync([FromRoute] int id)
        {
            var found = await _workItemService.GetByIdAsync(id);
            if (found == null)
            {
                return NotFound();
            }

            return Ok(found);
        }
    }
}
