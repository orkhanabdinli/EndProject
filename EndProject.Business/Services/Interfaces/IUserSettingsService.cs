using EndProject.Business.DTOs.UserSettingsDTOs.UserAboutDTOs;

namespace EndProject.Business.Services.Interfaces;

public interface IUserSettingsService
{
    Task<UserAboutGetDTO> UserAboutGetAsync(string UserId);
    //Task UpdateUserAboutAsync(string Id, UserAboutPutDTO userAboutPutDTO);
}
