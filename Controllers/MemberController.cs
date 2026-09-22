using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Requests;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Controllers
{
    [ApiController]
    [Authorize()]
    [Route("api/projects/{projectId}/members")]
    public class MemberController(IMemberService _memberService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(
            [FromRoute] int projectId,
            [FromQuery] UserFilter filter)
        {
            var foundList = await _memberService.GetAllAsync(projectId, filter);
            return Ok(foundList);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(
            [FromRoute] int projectId,
            [FromBody] MemberAddRequest request,
            [FromServices] IValidator<MemberAddRequest> validator)
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid) 
            {
                return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            await _memberService.AddAsync(projectId, request);
            return NoContent();
        }

        [HttpDelete("{memberId:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int projectId,
            [FromRoute] int memberId)
        {
            await _memberService.DeleteAsync(projectId, memberId);
            return NoContent();
        }
    }
}
