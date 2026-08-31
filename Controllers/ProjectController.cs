using Microsoft.AspNetCore.Mvc;
using TaskManagerWebApi.Models;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Response;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Controllers
{
    [ApiController]
    [Route("api/Projects")]
    public class ProjectController(IProjectService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery]ProjectFilter filter)
        {
            var foundList = await _service.GetAllAsync(filter);
            return Ok(foundList);
        }


        [HttpGet("{id:int}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<IActionResult> GetByIdAsync([FromRoute]int id)
        {
            var found = await _service.GetByIdAsync(id);
            return Ok(found);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]CreateProjectRequest request)
        {
            var created = await _service.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { id = created.Id },
                created
            );
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute]int id, [FromBody]UpdateProjectRequest request)
        {
            var updated = await _service.UpdateAsync(id, request);
            return Ok(updated);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
