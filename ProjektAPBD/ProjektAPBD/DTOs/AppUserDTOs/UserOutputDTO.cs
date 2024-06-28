using System.IdentityModel.Tokens.Jwt;

namespace ProjektAPBD.DTOs.AppUserDTOs;

public class UserOutputDTO
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    
}