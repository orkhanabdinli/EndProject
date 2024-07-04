using EndProject.Business.DTOs.UserSettingsDTOs.UserAboutDTOs;

namespace EndProject.Business.Services.Interfaces;

public interface IUserSettingsService
{
    Task<UserAboutGetDTO> UserAboutGetAsync(string UserId);
    Task UserAboutUpdateAsync(int Id, UserAboutPutDTO userAboutPutDTO);
}
