using Microsoft.AspNetCore.Http;

namespace EndProject.Business.DTOs.PostDTOs;

public class PostCreateDTO
{
    public string UserId { get; set; }
    public string Description { get; set; }
    public List<IFormFile>? PostMedias { get; set; }
}
