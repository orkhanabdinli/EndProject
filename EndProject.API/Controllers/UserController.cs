using EndProject.Business.DTOs;
using EndProject.Business.DTOs.UserDTOs;
using EndProject.Business.DTOs.UserProfileMediaDTOs;
using EndProject.Business.Services.Interfaces;
using EndProject.Business.Utilities.CustomExceptions.CommonExceptions;
using EndProject.Business.Utilities.CustomExceptions.NotFoundExceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        private readonly IUserSettingsService _userSettingsService;

        public UserController(RoleManager<IdentityRole> roleManager,
            IUserService userService,
            IUserProfileMediaService userProfileMediaService,
            IUserSettingsService userSettingsService)
        {
            _roleManager = roleManager;
            _userService = userService;
            _userProfileMediaService = userProfileMediaService;
            _userSettingsService = userSettingsService;
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
        [HttpGet("[action]/{UserId}")]
        public async Task<IActionResult> UserMediaGet(string UserId)
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
        [HttpGet("[action]")]
        public async Task<IActionResult> LogOut()
        {
            await _userService.LogOutAsync();
            return Ok();
        }
        [HttpPut("[action]/{UserId}")]
        public async Task<IActionResult> ChangePassword(string UserId, UserChangePasswordDTO userChangePasswordDTO)
        {
            try
            {
                await _userService.ChangePassword(UserId, userChangePasswordDTO);
                return Ok();
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(new ErrorDTO { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorDTO { Message = ex.Message });
            }
        }
    }
}
