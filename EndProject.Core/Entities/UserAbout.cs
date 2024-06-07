namespace EndProject.Core.Entities;

public class UserAbout : BaseEntity
{
    public string UserId { get; set; }
    public string Bio { get; set; }
    public string Country { get; set; }
    public AppUser User { get; set; }
}
