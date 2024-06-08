using EndProject.Core.Entities;
using EndProject.Core.Repositories;
using EndProject.Data.Contexts;

namespace EndProject.Data.Repositories;

public class UserAboutRepository : GenericRepository<UserAbout>, IUserAboutRepository
{
    public UserAboutRepository(AppDbContext context) : base(context)
    {
    }
}
