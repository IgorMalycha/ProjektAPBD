using System.IdentityModel.Tokens.Jwt;

namespace ProjektAPBD.DTOs.AppUserDTOs;

public class UserOutputDTO
{
    public JwtSecurityToken Token { get; set; }
    public string RefreshToken { get; set; }
    
}