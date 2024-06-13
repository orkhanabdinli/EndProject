using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [HttpGet("")]
        public async Task<IActionResult> CreateRole()
        {
            var role1 = new IdentityRole("Member");

            await _roleManager.CreateAsync(role1);

            return Ok();
        }
    }
}
