using EndProject.Core.Repositories;
using EndProject.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndProject.Data.ServiceRegistration;

public static class RepositoryRegistiration
{
    public static void AddRepository(this IServiceCollection services)
    {
        services.AddScoped<IUserAboutRepository, UserAboutRepository>();
        services.AddScoped<IFriendShipRepository, FriendShipRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ILikeRepository, LikeRepository>();
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IConversationRepository, ConversationRepository>();
        services.AddScoped<IAppUserRepository, AppUserRepository>();
    }

}
