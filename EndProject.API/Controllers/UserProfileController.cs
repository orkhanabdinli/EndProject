using EndProject.Business.DTOs;
using EndProject.Business.DTOs.UserSettingsDTOs.UserAboutDTOs;
using EndProject.Business.Services.Interfaces;
using EndProject.Business.Utilities.CustomExceptions.CommonExceptions;
using EndProject.Business.Utilities.CustomExceptions.NotFoundExceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EndProject.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserProfileController : ControllerBase
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IUserSettingsService _userSettingsService;

    public UserProfileController(RoleManager<IdentityRole> roleManager,
        IUserService userService,
        IUserProfileMediaService userProfileMediaService,
        IUserSettingsService userSettingsService)
    {
        _roleManager = roleManager;
        _userSettingsService = userSettingsService;
    }
    [HttpGet("[action]/{UserId}")]
    public async Task<IActionResult> UserAboutGet(string UserId)
    {
        try
        {
            return Ok(await _userSettingsService.UserAboutGetAsync(UserId));
        }
        catch (UserNotFoundException ex)
        {
            return BadRequest(new ErrorDTO { Message = ex.Message });
        }
        catch (UserAboutNotFoundException ex)
        {
            return BadRequest(new ErrorDTO { Message = ex.Message });
        }
        catch (UserProfileMediaNotFoundException ex)
        {
            return BadRequest(new ErrorDTO { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new ErrorDTO { Message = ex.Message });
        }
    }
    [HttpPut("[action]/{Id}")]
    public async Task<IActionResult> UserAboutUpdate(int Id, UserAboutPutDTO userAboutPutDTO)
    {
        try
        {
            await _userSettingsService.UserAboutUpdateAsync(Id, userAboutPutDTO);
            return Ok();
        }
        catch (InvalidIdException ex)
        {
            return BadRequest(new ErrorDTO { Message = ex.Message });
        }
        catch (UserAboutNotFoundException ex)
        {
            return BadRequest(new ErrorDTO { Message = ex.Message });
        }
        catch (UserProfileMediaNotFoundException ex)
        {
            return BadRequest(new ErrorDTO { Message = ex.Message });
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
}
