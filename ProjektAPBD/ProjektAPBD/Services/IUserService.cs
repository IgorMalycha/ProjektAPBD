using System.IdentityModel.Tokens.Jwt;
using ProjektAPBD.DTOs.AppUserDTOs;

namespace ProjektAPBD.Services;

public interface IUserService
{
    Task RegisterUser(RegisterRequestDTO requestDto);
    Task<UserOutputDTO> LoginUser(LoginRequestDTO loginRequestDto);
    Task<UserOutputDTO> GetRefreshToken(RefreshTokenRequestDTO refreshToken);
}