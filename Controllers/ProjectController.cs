using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Controllers
{
    [ApiController]
    [Route("api/Users/{userId}/Projects")]
    [Authorize]
    public class ProjectController(IProjectService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            [FromRoute] int userId, 
            [FromQuery] ProjectFilter filter)
        {
            var foundList = await _service.GetAllAsync(userId, filter);
            return Ok(foundList);
        }


        [HttpGet("{projectId:int}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int userId, 
            [FromRoute] int projectId)
        {
            var found = await _service.GetByIdAsync(userId, projectId);
            return Ok(found);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(
            [FromRoute] int userId,
            [FromBody] ProjectCreateRequest request,
            [FromServices] IValidator<ProjectCreateRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            var created = await _service.CreateAsync(userId, request);

            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { userId, projectId = created.Id },
                created
            );
        }


        [HttpPut("{projectId:int}")]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute] int userId,
            [FromRoute] int projectId, 
            [FromBody] ProjectUpdateRequest request,
            [FromServices] IValidator<ProjectUpdateRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            var updated = await _service.UpdateAsync(userId, projectId, request);
            return Ok(updated);
        }


        [HttpDelete("{projectId:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int userId, 
            [FromRoute]int projectId)
        {
            await _service.DeleteAsync(userId, projectId);
            return NoContent();
        }
    }
}
