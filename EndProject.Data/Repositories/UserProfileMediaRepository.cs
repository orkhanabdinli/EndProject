using EndProject.Core.Entities;
using EndProject.Core.Repositories;
using EndProject.Data.Contexts;

namespace EndProject.Data.Repositories;

public class UserProfileMediaRepository : GenericRepository<UserProfileMedia>, IUserProfileMediaRepository
{
    public UserProfileMediaRepository(AppDbContext context) : base(context)
    {
    }
}
