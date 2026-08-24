using Microsoft.AspNetCore.Mvc;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Services;

namespace TaskManagerWebApi.Controllers
{
    [ApiController]
    [Route("WorkItems")]
    public class WorkItemController(IWorkItemService _workItemService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery]WorkItemFilter filter)
        {
            var foundList = await _workItemService.GetAllAsync(filter);
            return Ok(foundList);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdASync([FromRoute]int id)
        {
            var found = await _workItemService.GetByIdAsync(id);
            return Ok(found);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]CreateWorkItemRequest request)
        {
            var created = _workItemService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetByIdASync),
                new { id = created.Id },
                created
            );
        }


        [HttpPut("{id:int}")]
        public async Task UpdateAsync([FromRoute]int id, [FromBody]UpdateWorkItemRequest request)
        {
            await _workItemService.UpdateAsync(id, request);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id)
        {
            await _workItemService.DeleteAsync(id);
            return NoContent();
        }
    }
}
