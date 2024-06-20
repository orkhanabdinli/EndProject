using EndProject.Business.DTOs;
using EndProject.Business.DTOs.UserDTOs;
using EndProject.Business.DTOs.UserProfileMediaDTOs;
using EndProject.Business.Services.Interfaces;
using EndProject.Business.Utilities.CustomExceptions.CommonExceptions;
using FluentValidation;
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
        private readonly IUserProfileMediaService _userProfileMediaService;

        public UserController(RoleManager<IdentityRole> roleManager,
            IUserService userService,
            IUserProfileMediaService userProfileMediaService)
        {
            _roleManager = roleManager;
            _userService = userService;
            _userProfileMediaService = userProfileMediaService;
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
                return BadRequest(new ErrorDTO { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDTO { Message = ex.Message });
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
                return BadRequest(new ErrorDTO { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDTO { Message = ex.Message });
            }
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> UserMedia(string UserId)
        {
            try
            {
                return Ok(await _userProfileMediaService.UserProfileMediaGetAsync(UserId));
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDTO { Message = ex.Message });
            }
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> UserMediaUpdate(UserProfileMediaPutDTO updateDTO)
        {
            try
            {
                await _userProfileMediaService.UpdateUserProfileMediaAsync(updateDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDTO { Message = ex.Message });
            }
        }
    }
}
