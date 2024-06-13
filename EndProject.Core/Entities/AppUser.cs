using Microsoft.AspNetCore.Identity;

namespace EndProject.Core.Entities;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
    public UserAbout UserAbout { get; set; }
    public List<Post>? Posts { get; set; }
    public List<FriendShip>? Friendship1 { get; set; }
    public List<FriendShip>? Friendship2 { get; set; }
    public List<Comment>? Comments { get; set; }
    public List<Like>? Likes { get; set; }
    public List<Conversation>? ConversationUser1 { get; set; }
    public List<Conversation>? ConversationUser2 { get; set; }
    public List<Message>? Messages { get; set; }
    public List<UserProfileMedia>? UserProfileMedias { get; set; }
}
