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
    [Route("api/projects")]
    [Authorize]
    public class ProjectController(IProjectService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] ProjectFilter filter)
        {
            var foundList = await _service.GetAllAsync(filter);
            return Ok(foundList);
        }


        [HttpGet("{projectId:int}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int projectId)
        {
            var found = await _service.GetByIdAsync(projectId);
            return Ok(found);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync(
            [FromBody] ProjectCreateRequest request,
            [FromServices] IValidator<ProjectCreateRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            var created = await _service.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { projectId = created.Id },
                created
            );
        }


        [HttpPut("{projectId:int}")]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute] int projectId, 
            [FromBody] ProjectUpdateRequest request,
            [FromServices] IValidator<ProjectUpdateRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            var updated = await _service.UpdateAsync(projectId, request);
            return Ok(updated);
        }


        [HttpDelete("{projectId:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int projectId)
        {
            await _service.DeleteAsync(projectId);
            return NoContent();
        }
    }
}
