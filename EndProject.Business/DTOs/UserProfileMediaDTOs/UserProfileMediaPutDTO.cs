using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndProject.Business.DTOs.UserProfileMediaDTOs
{
    public class UserProfileMediaPutDTO
    {
        public string UserId { get; set; }
        public IFormFile ProfileImage { get; set; }
        public IFormFile BackgroundImage { get; set; }
    }
}
