using ProjektAPBD.DTOs.AppUserDTOs;
using ProjektAPBD.Models;

namespace ProjektAPBD.Repository;

public interface IUserRepository
{
    Task<bool> DoesLoginExist(string login);

    Task AddUser(User newUser);
    Task<User> GetUserByLogin(LoginRequestDTO loginRequestDto);
    Task<string> AddUserRefreshToken(User user);
    Task<User?> GetUserByRefreshToken(RefreshTokenRequestDTO refreshTokenRequestDto);
}