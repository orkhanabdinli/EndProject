namespace EndProject.Core.Entities;

public class FriendShip : BaseEntity
{
    public string User1Id { get; set; }
    public string User2Id { get; set; }
    public bool Status { get; set; }
    public AppUser User1 { get; set; }
    public AppUser User2 { get; set; }
}
