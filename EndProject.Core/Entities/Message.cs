namespace EndProject.Core.Entities;

public class Message : BaseEntity
{
    public int ConversationId { get; set; }
    public string UserId { get; set; }
    public string Text { get; set; }
    public Conversation Conversation { get; set; }
    public AppUser User { get; set; }
}
