using EndProject.Business.DTOs.TokenDTOs;
using EndProject.Business.DTOs.UserDTOs;
using EndProject.Business.Services.Interfaces;
using EndProject.Business.Utilities.CustomExceptions.CommonExceptions;
using EndProject.Core.Entities;
using EndProject.Core.Repositories;
using EndProject.Data.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace EndProject.Business.Services.Implementations;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IAppUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;
    private readonly IUserAboutRepository _userAboutRepository;
    private readonly IUserProfileMediaRepository _userProfileMediaRepository;

    public UserService(AppDbContext context,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        ITokenService tokenService,
        IAppUserRepository userRepository,
        IConfiguration configuration,
        IWebHostEnvironment env,
        IUserProfileMediaRepository userProfileMediaRepository,
        IUserAboutRepository userAboutRepository)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _userRepository = userRepository;
        _configuration = configuration;
        _env = env;
        _userProfileMediaRepository = userProfileMediaRepository;
        _userAboutRepository = userAboutRepository;
    }

    public async Task<TokenResponseDTO> LoginAsync(UserLoginDTO userLoginDTO)
    {
        var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);
        if (user is null) throw new InvalidCredentialsException(401, "Incorrect username or password");
        var result = await _signInManager.PasswordSignInAsync(user, userLoginDTO.Password, false, false);
        if (!result.Succeeded) throw new InvalidCredentialsException("Incorrect username or password");

        var tokens = await _tokenService.GenerateTokenAsync(user);
        tokens.UserId = user.Id;
        return tokens;
    }

    public async Task RegisterAsync(UserRegisterDTO userRegisterDTO)
    {
        var user = _userRepository.Table.FirstOrDefault(x => x.Email == userRegisterDTO.Email);
        if (user is not null) throw new AlreadyExistException(403, "Email already exist");
        var user1 = _userRepository.Table.FirstOrDefault(x => x.UserName == userRegisterDTO.UserName);
        if (user1 is not null) throw new AlreadyExistException(403, "Username already exist");
        AppUser appUser = new()
        {
            FirstName = userRegisterDTO.FirstName,
            LastName = userRegisterDTO.LastName,
            UserName = userRegisterDTO.UserName,
            Email = userRegisterDTO.Email,
            IsActive = true,
            CreatedDate = DateTime.UtcNow.AddHours(4)
        };

        var result = await _userManager.CreateAsync(appUser, userRegisterDTO.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                throw new Exception(error.Description);
            }
        }
        var result2 = await _userManager.AddToRoleAsync(appUser, "Member");
        if (!result2.Succeeded)
        {
            foreach (var error in result2.Errors)
            {
                throw new Exception(error.Description);
            }
        }
        UserAbout userAbout = new()
        {
            UserId = appUser.Id,
            Gender = userRegisterDTO.Gender,
            Country = userRegisterDTO.Country,
            City = userRegisterDTO.City,
            CreatedDate = DateTime.UtcNow.AddHours(4),
            UpdatedDate = DateTime.UtcNow.AddHours(4)
        };

        UserProfileMedia profileImage = new UserProfileMedia()
        {
            UserId = appUser.Id,
            ImageUrl = $"{_env.WebRootPath}/DefaultImages/DefaultProfileImage.jpg",
            IsBackgroundImage = false,
            CreatedDate = DateTime.UtcNow.AddHours(4),
            UpdatedDate = DateTime.UtcNow.AddHours(4)
        };
        UserProfileMedia backgroundImage = new UserProfileMedia()
        {
            UserId = appUser.Id,
            ImageUrl = $"{_env.WebRootPath}/DefaultImages/DefaultBackgroundImage.jpg",
            IsBackgroundImage = true,
            CreatedDate = DateTime.UtcNow.AddHours(4),
            UpdatedDate = DateTime.UtcNow.AddHours(4)
        };
        await _userAboutRepository.Table.AddAsync(userAbout);
        await _userProfileMediaRepository.Table.AddAsync(profileImage);
        await _userProfileMediaRepository.Table.AddAsync(backgroundImage);
        await _context.SaveChangesAsync();
    }

    public async Task LogOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
