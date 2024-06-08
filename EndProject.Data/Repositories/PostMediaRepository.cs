using EndProject.Core.Entities;
using EndProject.Core.Repositories;
using EndProject.Data.Contexts;

namespace EndProject.Data.Repositories;

public class PostMediaRepository : GenericRepository<PostMedia>, IPostMediaRepository
{
    public PostMediaRepository(AppDbContext context) : base(context)
    {
    }
}
