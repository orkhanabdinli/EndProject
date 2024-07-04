using EndProject.Business.DTOs.UserProfileMediaDTOs;
using EndProject.Business.Services.Interfaces;
using EndProject.Business.Utilities.CustomExceptions.NotFoundExceptions;
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
    }
}
