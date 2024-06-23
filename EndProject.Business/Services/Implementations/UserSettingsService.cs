using AutoMapper;
using EndProject.Business.DTOs.UserSettingsDTOs.UserAboutDTOs;
using EndProject.Business.Services.Interfaces;
using EndProject.Business.Utilities.CustomExceptions.CommonExceptions;
using EndProject.Business.Utilities.CustomExceptions.NotFoundExceptions;
using EndProject.Core.Entities;
using EndProject.Core.Repositories;
using EndProject.Data.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.Metrics;

namespace EndProject.Business.Services.Implementations;

public class UserSettingsService : IUserSettingsService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserAboutRepository _userAboutRepository;
    private readonly IUserProfileMediaRepository _userProfileMediaRepository;
    //private readonly IMapper _mapper;

    public UserSettingsService(UserManager<AppUser> userManager,
        IUserAboutRepository userAboutRepository,
        IUserProfileMediaRepository userProfileMediaRepository
        /*IMapper mapper*/)
    {
        _userManager = userManager;
        _userProfileMediaRepository = userProfileMediaRepository;
        _userAboutRepository = userAboutRepository;
        //_mapper = mapper;
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
    //public async Task UpdateUserAboutAsync(string Id, UserAboutPutDTO userAboutPutDTO)
    //{
    //    if (Id != userAboutPutDTO.Id) throw new InvalidIdException(400, "Invalid Id");
    //    var ExistingUserAbout = await _userAboutRepository.GetSingleAsync(x => x.UserId == userAboutPutDTO.UserId && x.IsActive == true);
    //    if (ExistingUserAbout is null) throw new UserAboutNotFoundException(404, "UserAbout is not found");
    //    var UserAbout = _mapper.Map(userAboutPutDTO, ExistingUserAbout);
    //    UserAbout.UpdatedDate = DateTime.UtcNow;
    //    await _userAboutRepository.CommitAsync();
    //}
}
