namespace EndProject.Business.DTOs.TokenDTOs;

public class TokenResponseDTO
{
    public string UserId { get; set; }
    public string AccesToken { get; set; }
    public DateTime ExpirationDate { get; set; }
}
