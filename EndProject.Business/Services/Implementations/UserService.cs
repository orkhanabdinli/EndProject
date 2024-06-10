using EndProject.Business.DTOs.TokenDTOs;
using EndProject.Business.DTOs.UserDTOs;
using EndProject.Business.Services.Interfaces;
using EndProject.Business.Utilities.CustomExceptions.CommonExceptions;
using EndProject.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace EndProject.Business.Services.Implementations;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public UserService(UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    public async Task<TokenResponseDTO> LoginAsync(UserLoginDTO userLoginDTO)
    {
        var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);
        if (user is null) throw new InvalidCredentialsException(401, "Incorrect username or password");
        var result = await _signInManager.PasswordSignInAsync(user, userLoginDTO.Password, false, false);
        if (!result.Succeeded) throw new InvalidCredentialsException("Invalid Credentials");
        
        var tokens = await _tokenService.GenerateTokenAsync(user);
        tokens.UserId = user.Id;
        return tokens;
    }

    public Task RegisterAsync(UserRegisterDTO userRegisterDTO)
    {
        throw new NotImplementedException();
    }
}
