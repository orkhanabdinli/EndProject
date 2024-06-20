using EndProject.Business.DTOs.UserProfileMediaDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndProject.Business.Services.Interfaces
{
    public interface IUserProfileMediaService
    {
        Task<UserProfileMediaGetDTO> UserProfileMediaGetAsync(string userId);
        Task UpdateUserProfileMediaAsync(UserProfileMediaPutDTO updateDTO);
    }
}
