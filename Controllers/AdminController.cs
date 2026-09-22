using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController(IAdminService _service) : ControllerBase
    {
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserFilter filter)
        {
            var foundList = await _service.GetAllUsers(filter);
            return Ok(foundList);
        }
    }
}
