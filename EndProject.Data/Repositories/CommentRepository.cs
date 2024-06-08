using EndProject.Core.Entities;
using EndProject.Core.Repositories;
using EndProject.Data.Contexts;

namespace EndProject.Data.Repositories;

public class CommentRepository : GenericRepository<Comment>, ICommentRepository
{
    public CommentRepository(AppDbContext context) : base(context)
    {
    }
}
