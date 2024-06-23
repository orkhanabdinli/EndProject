namespace EndProject.Business.DTOs.UserSettingsDTOs.UserAboutDTOs;

public class UserAboutGetDTO
{
    public int UserAboutId { get; set; }
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Gender { get; set; }
    public string? Bio { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string ProfileImageUrl { get; set; }
    public string BackgroundImageUrl { get; set; }
}
