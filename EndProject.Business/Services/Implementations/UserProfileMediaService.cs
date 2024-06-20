using EndProject.Business.DTOs.UserProfileMediaDTOs;
using EndProject.Business.Services.Interfaces;
using EndProject.Business.Utilities.CustomExceptions.NotFoundExceptions;
using EndProject.Business.Utilities.Extensions;
using EndProject.Core.Entities;
using EndProject.Core.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace EndProject.Business.Services.Implementations
{
    public class UserProfileMediaService : IUserProfileMediaService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserProfileMediaRepository _userProfileMediaRepository;
        private readonly IWebHostEnvironment _env;
        private readonly string _rootPath;
        private readonly string _folderName = "UserProfileMedias";

        public UserProfileMediaService(UserManager<AppUser> userManager,
            IUserProfileMediaRepository userProfileMediaRepository,
            IConfiguration configuration,
            IWebHostEnvironment env)
        {
            _userManager = userManager;
            _userProfileMediaRepository = userProfileMediaRepository;
            _env = env;
            _rootPath = $"{_env.WebRootPath}";
        }
        public async Task<UserProfileMediaGetDTO> UserProfileMediaGetAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) throw new UserNotFoundException(404, "User is not found");

            var userProfileMedias = await _userProfileMediaRepository.GetAllAsync(x => x.UserId == user.Id);
            if (userProfileMedias == null || !userProfileMedias.Any()) throw new UserProfileMediaNotFoundException(404, "UserProfileMedia is not found");

            var profileImage = userProfileMedias.FirstOrDefault(x => !x.IsBackgroundImage);
            var backgroundImage = userProfileMedias.FirstOrDefault(x => x.IsBackgroundImage);

            var userProfileMediaGetDTO = new UserProfileMediaGetDTO
            {
                UserId = user.Id,
                ProfileImageUrl = profileImage?.ImageUrl,
                BackgroundImageUrl = backgroundImage?.ImageUrl
            };

            return userProfileMediaGetDTO;
        }
        public async Task UpdateUserProfileMediaAsync(UserProfileMediaPutDTO updateDTO)
        {
            var user = await _userManager.FindByIdAsync(updateDTO.UserId);
            if (user == null) throw new UserNotFoundException(404, "User is not found");

            var userProfileMedias = await _userProfileMediaRepository.GetAllAsync(x => x.UserId == user.Id);
            if (userProfileMedias == null || !userProfileMedias.Any()) throw new UserProfileMediaNotFoundException(404, "UserProfileMedia is not found");

            var profileImage = userProfileMedias.FirstOrDefault(x => !x.IsBackgroundImage);
            var backgroundImage = userProfileMedias.FirstOrDefault(x => x.IsBackgroundImage);

            if (profileImage != null && updateDTO.ProfileImage != null)
            {
                // Delete old profile image
                if (!string.IsNullOrEmpty(profileImage.ImageUrl))
                {
                    FileManager.DeleteFile(_rootPath, _folderName, profileImage.ImageUrl);
                }

                // Save new profile image
                var newProfileImageUrl = updateDTO.ProfileImage.SaveFile(_rootPath, _folderName);
                profileImage.ImageUrl = newProfileImageUrl;
            }

            if (backgroundImage != null && updateDTO.BackgroundImage != null)
            {
                // Delete old background image
                if (!string.IsNullOrEmpty(backgroundImage.ImageUrl))
                {
                    FileManager.DeleteFile(_rootPath, _folderName, backgroundImage.ImageUrl);
                }

                // Save new background image
                var newBackgroundImageUrl = updateDTO.BackgroundImage.SaveFile(_rootPath, _folderName);
                backgroundImage.ImageUrl = newBackgroundImageUrl;
            }

            // Update database records directly in the service
            await _userProfileMediaRepository.CommitAsync();
        }
    }
}
