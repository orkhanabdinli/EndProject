using AutoMapper;
using EndProject.Business.DTOs.UserDTOs;
using EndProject.Business.DTOs.UserSettingsDTOs.UserAboutDTOs;
using EndProject.Business.Services.Interfaces;
using EndProject.Business.Utilities.CustomExceptions.CommonExceptions;
using EndProject.Business.Utilities.CustomExceptions.NotFoundExceptions;
using EndProject.Business.Utilities.Extensions;
using EndProject.Core.Entities;
using EndProject.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EndProject.Business.Services.Implementations;

public class UserSettingsService : IUserSettingsService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserAboutRepository _userAboutRepository;
    private readonly IAppUserRepository _userRepository;
    private readonly IUserProfileMediaRepository _userProfileMediaRepository;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;
    private readonly string _rootPath;
    private readonly string _folderName = "UserProfileMedias";

    public UserSettingsService(UserManager<AppUser> userManager,
        IUserAboutRepository userAboutRepository,
        IAppUserRepository userRepository,
        IUserProfileMediaRepository userProfileMediaRepository,
        IMapper mapper,
        IWebHostEnvironment env)
    {
        _userManager = userManager;
        _userProfileMediaRepository = userProfileMediaRepository;
        _userAboutRepository = userAboutRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _env = env;
        _rootPath = $"{_env.WebRootPath}";
    }
    public async Task<UserAboutGetDTO> UserAboutGetAsync(string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        
        var userAbout = await _userAboutRepository.GetSingleAsync(x => x.UserId == user.Id && x.IsActive == true);
        if (userAbout is null) throw new UserAboutNotFoundException(404, "UserAbout is not found");
        
        var userProfileMedias = await _userProfileMediaRepository.GetAllAsync(x => x.UserId == user.Id);
        if (userProfileMedias is null) throw new UserProfileMediaNotFoundException(404, "UserProfileMedia is not found");

        var profileImage = userProfileMedias.FirstOrDefault(x => !x.IsBackgroundImage);
        var backgroundImage = userProfileMedias.FirstOrDefault(x => x.IsBackgroundImage);

        UserAboutGetDTO userAboutGet = new UserAboutGetDTO()
        {
            UserAboutId = userAbout.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            UserId = UserId,
            Gender = userAbout.Gender,
            Bio = userAbout.Bio,
            Country = userAbout.Country,
            City = userAbout.City,
            ProfileImageUrl = profileImage.ImageUrl,
            BackgroundImageUrl = backgroundImage.ImageUrl
        };
        return userAboutGet;
    }
    public async Task UserAboutUpdateAsync(int Id, UserAboutPutDTO userAboutPutDTO)
    {
        if (Id != userAboutPutDTO.Id) throw new InvalidIdException(400, "Invalid Id");
        var existingUserAbout = await _userAboutRepository.GetSingleAsync(x => x.UserId == userAboutPutDTO.UserId && x.IsActive == true);
        if (existingUserAbout is null) throw new UserAboutNotFoundException(404, "UserAbout is not found");
        var userProfileMedias = await _userProfileMediaRepository.GetAllAsync(x => x.UserId == existingUserAbout.UserId);
        if (userProfileMedias == null || !userProfileMedias.Any()) throw new UserProfileMediaNotFoundException(404, "UserProfileMedia is not found");
        var user1 = _userRepository.Table.FirstOrDefault(x => x.Id == existingUserAbout.UserId);
        var user2 = _userRepository.Table.FirstOrDefault(x => x.UserName == userAboutPutDTO.UserName);
        if (userAboutPutDTO.UserName != user1.UserName && user2 is not null) throw new AlreadyExistException(404, "Username already exist");
        var profileImage = userProfileMedias.FirstOrDefault(x => !x.IsBackgroundImage);
        var backgroundImage = userProfileMedias.FirstOrDefault(x => x.IsBackgroundImage);

        if (profileImage != null && userAboutPutDTO.ProfileImageUrl != null)
        {
            // Delete old profile image
            if (!string.IsNullOrEmpty(profileImage.ImageUrl))
            {
                FileManager.DeleteFile(_rootPath, _folderName, profileImage.ImageUrl);
            }

            // Save new profile image
            var newProfileImageUrl = userAboutPutDTO.ProfileImageUrl.SaveFile(_rootPath, _folderName);
            profileImage.ImageUrl = newProfileImageUrl;
        }

        if (backgroundImage != null && userAboutPutDTO.BackgroundImageUrl != null)
        {
            // Delete old background image
            if (!string.IsNullOrEmpty(backgroundImage.ImageUrl))
            {
                FileManager.DeleteFile(_rootPath, _folderName, backgroundImage.ImageUrl);
            }

            // Save new background image
            var newBackgroundImageUrl = userAboutPutDTO.BackgroundImageUrl.SaveFile(_rootPath, _folderName);
            backgroundImage.ImageUrl = newBackgroundImageUrl;
        }

        user1.UserName = userAboutPutDTO.UserName;
        existingUserAbout.Gender = userAboutPutDTO.Gender;
        existingUserAbout.Bio = userAboutPutDTO.Bio;
        existingUserAbout.Country = userAboutPutDTO.Country;
        existingUserAbout.City = userAboutPutDTO.City;
        existingUserAbout.UpdatedDate = DateTime.UtcNow;
        await _userAboutRepository.CommitAsync();
    }
}
