using AutoMapper;
using EndProject.Business.DTOs.UserSettingsDTOs.UserAboutDTOs;
using EndProject.Business.Services.Interfaces;
using EndProject.Business.Utilities.CustomExceptions.CommonExceptions;
using EndProject.Business.Utilities.CustomExceptions.NotFoundExceptions;
using EndProject.Core.Entities;
using EndProject.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace EndProject.Business.Services.Implementations;

public class UserSettingsService : IUserSettingsService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserAboutRepository _userAboutRepository;
    private readonly IMapper _mapper;

    public UserSettingsService(UserManager<AppUser> userManager,
        IUserAboutRepository userAboutRepository,
        IMapper mapper)
    {
        _userManager = userManager;
        _userAboutRepository = userAboutRepository;
        _mapper = mapper;
    }
    public async Task<UserAboutGetDTO> UserAboutGetAsync(string UserId)
    {
        var user = await _userManager.FindByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException(404, "User is not found");
        var userAbout = await _userAboutRepository.GetSingleAsync(x => x.UserId == user.Id && x.IsActive == true);
        if (userAbout is null) throw new UserAboutNotFoundException(404, "UserAbout is not found");
        var userAboutGetDTO = _mapper.Map<UserAboutGetDTO>(userAbout);
        userAboutGetDTO.UserAboutId = userAbout.Id;
        return userAboutGetDTO;
    }
    public async Task UpdateUserAboutAsync(int Id, UserAboutPutDTO userAboutPutDTO)
    {
        if (Id != userAboutPutDTO.Id) throw new InvalidIdException(400, "Invalid Id");
        var ExistingUserAbout = await _userAboutRepository.GetSingleAsync(x => x.UserId == userAboutPutDTO.UserId && x.IsActive == true);
        if (ExistingUserAbout is null) throw new UserAboutNotFoundException(404, "UserAbout is not found");
        var UserAbout = _mapper.Map(userAboutPutDTO, ExistingUserAbout);
        UserAbout.UpdatedDate = DateTime.UtcNow;
        await _userAboutRepository.CommitAsync();
    }
}
