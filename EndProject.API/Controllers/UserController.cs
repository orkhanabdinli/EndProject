using EndProject.Business.DTOs.UserDTOs;
using EndProject.Business.Services.Interfaces;
using EndProject.Business.Utilities.CustomExceptions.CommonExceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace EndProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        public UserController(RoleManager<IdentityRole> roleManager,
            IUserService userService)
        {
            _roleManager = roleManager;
            _userService = userService;
        }
        //[HttpGet("")]
        //public async Task<IActionResult> CreateRole()
        //{
        //    var role1 = new IdentityRole("Member");

        //    await _roleManager.CreateAsync(role1);

        //    return Ok();
        //}

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(UserLoginDTO loginDTO)
        {
            try
            {
                return Ok(await _userService.LoginAsync(loginDTO));
            }
            catch (InvalidCredentialException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(UserRegisterDTO registerDTO)
        {
            try
            {
                await _userService.RegisterAsync(registerDTO);
                return Ok();
            }
            catch (AlreadyExistException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
