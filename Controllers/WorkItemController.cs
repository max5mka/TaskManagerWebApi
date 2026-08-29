using Microsoft.AspNetCore.Mvc;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Controllers
{
    [ApiController]
    [Route("api/Projects/{projectId:int}/WorkItems")]
    public class WorkItemController(IWorkItemService _workItemService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            [FromRoute] int projectId,
            [FromQuery] WorkItemFilter filter)
        {
            var foundList = await _workItemService.GetAllAsync(projectId, filter);
            return Ok(foundList);
        }


        [HttpGet("{workItemId:int}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int projectId, 
            [FromRoute] int workItemId)
        {
            var found = await _workItemService.GetByIdAsync(projectId, workItemId);
            return Ok(found);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(
            [FromRoute] int projectId, 
            [FromBody] CreateWorkItemRequest request)
        {
            var created = await _workItemService.CreateAsync(projectId, request);

            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { projectId, created.Id },
                created
            );
        }


        [HttpPut("{workItemId:int}")]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute] int projectId,
            [FromRoute] int workItemId,
            [FromBody] UpdateWorkItemRequest request)
        {
            var updated = await _workItemService.UpdateAsync(projectId, workItemId, request);
            return Ok(updated);
        }


        [HttpDelete("{workItemId:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int projectId,
            [FromRoute] int workItemId)
        {
            await _workItemService.DeleteAsync(projectId, workItemId);
            return NoContent();
        }
    }
}
