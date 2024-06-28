using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ProjektAPBD.DTOs.AppUserDTOs;
using ProjektAPBD.Models;
using ProjektAPBD.Repository;
using ProjektAPBD.SecurityHelpers;

namespace ProjektAPBD.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task RegisterUser(RegisterRequestDTO requestDto)
    {
        var hashedPasswordAndSalt = SecurityHelper.GetHashedPasswordAndSalt(requestDto.Password);

        await DoesLoginExist(requestDto.Login);
        
        var newUser = new User()
        {
            Login = requestDto.Login,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelper.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        };

        await _userRepository.AddUser(newUser);

    }

    public async Task<UserOutputDTO> LoginUser(LoginRequestDTO loginRequestDto)
    {
        await DoesLoginExist(loginRequestDto.Login);
        
        User user = await _userRepository.GetUserByLogin(loginRequestDto);

        string passwordHashFromDb = user.Password;
        string curHashedPassword = SecurityHelper.GetHashedPasswordWithSalt(loginRequestDto.Password, user.Salt);

        if (passwordHashFromDb != curHashedPassword)
        {
            throw new UnauthorizedAccessException("Wrong password");
        }
        
        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, "IgorAdmin"),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "admin")
            
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretKey"));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );


        var newRefreshToken = await _userRepository.AddUserRefreshToken(user);
        
        return new UserOutputDTO()
        {
            Token = token,
            RefreshToken = newRefreshToken
        };
    }

    public async Task<UserOutputDTO> GetRefreshToken(RefreshTokenRequestDTO refreshTokenRequestDto)
    {
        User? user = await _userRepository.GetUserByRefreshToken(refreshTokenRequestDto);

        await DoesRefreshTokenExist(user);

        await IsRefreshTokenExpLessThanActualDate(user);
        
        Claim[] userclaim = new[]
        {
            new Claim(ClaimTypes.Name, "igorAdmin"),
            new Claim(ClaimTypes.Role, "user"),
            new Claim(ClaimTypes.Role, "admin")
        };

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("secretKey"));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtToken = new JwtSecurityToken(
            issuer: "https://localhost:5001",
            audience: "https://localhost:5001",
            claims: userclaim,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        string refreshToken = await _userRepository.AddUserRefreshToken(user);

        return new UserOutputDTO()
        {
            Token = jwtToken,
            RefreshToken = refreshToken
        };
    }

    private async Task IsRefreshTokenExpLessThanActualDate(User user)
    {
        if (user.RefreshTokenExp < DateTime.Now)
        {
            throw new SecurityTokenException("Refresh token expired");
        }
    }

    private async Task DoesRefreshTokenExist(User? user)
    {
        if (user == null)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }
    }

    private async Task DoesLoginExist(string requestDtoLogin)
    {
        if (await _userRepository.DoesLoginExist(requestDtoLogin))
        {
            throw new ArgumentException($"Login: {requestDtoLogin} already exists");
        }
    }
}