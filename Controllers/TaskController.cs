using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Controllers
{
    [ApiController]
    [Route("api/Users/{userId}/Projects/{projectId:int}/Tasks")]
    [Authorize]
    public class TaskController(ITaskService _taskService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            [FromRoute] int userId,
            [FromRoute] int projectId,
            [FromQuery] TaskFilter filter)
        {
            var foundList = await _taskService.GetAllAsync(userId, projectId, filter);
            return Ok(foundList);
        }


        [HttpGet("{taskId:int}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int userId,
            [FromRoute] int projectId, 
            [FromRoute] int taskId)
        {
            var found = await _taskService.GetByIdAsync(userId, projectId, taskId);
            return Ok(found);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(
            [FromRoute] int userId,
            [FromRoute] int projectId, 
            [FromBody] TaskCreateRequest request,
            [FromServices] IValidator<TaskCreateRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            var created = await _taskService.CreateAsync(userId, projectId, request);

            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { userId, projectId, taskId = created.Id },
                created
            );
        }


        [HttpPut("{taskId:int}")]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute] int userId,
            [FromRoute] int projectId,
            [FromRoute] int taskId,
            [FromBody] TaskUpdateRequest request,
            [FromServices] IValidator<TaskUpdateRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            var updated = await _taskService.UpdateAsync(userId, projectId, taskId, request);
            return Ok(updated);
        }


        [HttpDelete("{taskId:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int userId,
            [FromRoute] int projectId,
            [FromRoute] int taskId)
        {
            await _taskService.DeleteAsync(userId, projectId, taskId);
            return NoContent();
        }
    }
}
