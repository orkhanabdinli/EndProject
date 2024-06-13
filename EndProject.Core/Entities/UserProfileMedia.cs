using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndProject.Core.Entities
{
    public class UserProfileMedia : BaseEntity
    {
        public string? UserId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsBackgroundImage { get; set; }
        public AppUser User { get; set; }
    }
}
