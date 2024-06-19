using EndProject.Business.DTOs.TokenDTOs;
using EndProject.Business.DTOs.UserDTOs;

namespace EndProject.Business.Services.Interfaces;

public interface IUserService
{
    Task<TokenResponseDTO> LoginAsync(UserLoginDTO userLoginDTO);
    Task RegisterAsync(UserRegisterDTO userRegisterDTO);
    Task LogOutAsync();
}
