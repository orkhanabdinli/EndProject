using EndProject.Core.Entities;
using EndProject.Core.Repositories;
using EndProject.Data.Contexts;

namespace EndProject.Data.Repositories;

public class FriendShipRepository : GenericRepository<FriendShip>, IFriendShipRepository
{
    public FriendShipRepository(AppDbContext context) : base(context)
    {
    }
}
