using EndProject.Business.DTOs.TokenDTOs;
using EndProject.Core.Entities;

namespace EndProject.Business.Services.Interfaces;

public interface ITokenService
{
    Task<TokenResponseDTO> GenerateTokenAsync(AppUser user);
}
