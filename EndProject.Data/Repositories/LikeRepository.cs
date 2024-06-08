using EndProject.Core.Entities;
using EndProject.Core.Repositories;
using EndProject.Data.Contexts;

namespace EndProject.Data.Repositories;

public class LikeRepository : GenericRepository<FriendShip>, IFriendShipRepository
{
    public LikeRepository(AppDbContext context) : base(context)
    {
    }
}
