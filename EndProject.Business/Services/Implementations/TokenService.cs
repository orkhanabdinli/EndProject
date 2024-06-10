using EndProject.Business.DTOs.TokenDTOs;
using EndProject.Business.Services.Interfaces;
using EndProject.Core.Entities;
using EndProject.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EndProject.Business.Services.Implementations;

public class TokenService : ITokenService
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    public TokenService(AppDbContext context,
                        UserManager<AppUser> userManager,
                        IConfiguration configuration)
    {
        _context = context;
        _userManager = userManager;
        _configuration = configuration;
    }
    public async Task<TokenResponseDTO> GenerateTokenAsync(AppUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:securityKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> Claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Name,user.UserName),
            new Claim(JwtRegisteredClaimNames.Email,user.Email),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };
        var roles = await _userManager.GetRolesAsync(user);
        Claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        var token = new JwtSecurityToken(
            _configuration["JWT:issuer"],
            _configuration["JWT:audience"],
            claims: Claims,
            expires: DateTime.UtcNow.AddHours(4),
            signingCredentials: credentials);

        var AccessToken = new JwtSecurityTokenHandler().WriteToken(token);

        TokenResponseDTO tokenDTO = new TokenResponseDTO()
        {
            AccesToken = AccessToken,
            ExpirationDate = DateTime.UtcNow.AddHours(5)
        };
        
        await _context.SaveChangesAsync();
        return tokenDTO;
    }
}
