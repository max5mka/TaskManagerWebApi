using Microsoft.AspNetCore.Mvc;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Controllers
{
    [ApiController]
    [Route("api/Projects/{projectId:int}/Tasks")]
    public class TaskController(ITaskService _taskService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            [FromRoute] int projectId,
            [FromQuery] TaskFilter filter)
        {
            var foundList = await _taskService.GetAllAsync(projectId, filter);
            return Ok(foundList);
        }


        [HttpGet("{taskId:int}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int projectId, 
            [FromRoute] int taskId)
        {
            var found = await _taskService.GetByIdAsync(projectId, taskId);
            return Ok(found);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(
            [FromRoute] int projectId, 
            [FromBody] CreateTaskRequest request)
        {
            var created = await _taskService.CreateAsync(projectId, request);

            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { projectId, taskId = created.Id },
                created
            );
        }


        [HttpPut("{taskId:int}")]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute] int projectId,
            [FromRoute] int taskId,
            [FromBody] UpdateTaskRequest request)
        {
            var updated = await _taskService.UpdateAsync(projectId, taskId, request);
            return Ok(updated);
        }


        [HttpDelete("{taskId:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int projectId,
            [FromRoute] int taskId)
        {
            await _taskService.DeleteAsync(projectId, taskId);
            return NoContent();
        }
    }
}
