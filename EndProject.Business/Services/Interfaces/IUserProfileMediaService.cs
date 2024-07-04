using EndProject.Business.DTOs.UserProfileMediaDTOs;

namespace EndProject.Business.Services.Interfaces
{
    public interface IUserProfileMediaService
    {
        Task<UserProfileMediaGetDTO> UserProfileMediaGetAsync(string userId);
    }
}
