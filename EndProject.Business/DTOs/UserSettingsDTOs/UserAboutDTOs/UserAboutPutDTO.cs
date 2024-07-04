using Microsoft.AspNetCore.Http;

namespace EndProject.Business.DTOs.UserSettingsDTOs.UserAboutDTOs;

public class UserAboutPutDTO
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Gender { get; set; }
    public string? Bio { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public IFormFile? ProfileImageUrl { get; set; }
    public IFormFile? BackgroundImageUrl { get; set; }
}
