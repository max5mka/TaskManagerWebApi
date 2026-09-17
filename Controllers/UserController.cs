using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using TaskManagerWebApi.Models.Filters;
using TaskManagerWebApi.Models.Services.Interfaces;

namespace TaskManagerWebApi.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UserController(IUserService _service) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] UserFilter filter)
        {
            var foundList = await _service.GetAllAsync(filter);
            return Ok(foundList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var found = await _service.GetByIdAsync(id);
            return Ok(found);
        }
    }
}
