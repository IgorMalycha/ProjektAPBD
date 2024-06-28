using Microsoft.EntityFrameworkCore;
using ProjektAPBD.Context;
using ProjektAPBD.DTOs.AppUserDTOs;
using ProjektAPBD.Models;
using ProjektAPBD.SecurityHelpers;

namespace ProjektAPBD.Repository;

public class UserRepository : IUserRepository
{
    private readonly DatabaseContext _databaseContext;

    public UserRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task<bool> DoesLoginExist(string login)
    {
        return await _databaseContext.Users.AnyAsync(e => e.Login == login);
    }

    public async Task AddUser(User newUser)
    {
        await _databaseContext.Users.AddAsync(newUser);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<User> GetUserByLogin(LoginRequestDTO loginRequestDto)
    {
        return await _databaseContext.Users.FirstOrDefaultAsync(e => e.Login == loginRequestDto.Login);
    }

    public async Task<string> AddUserRefreshToken(User user)
    {
        string refreshToken = SecurityHelper.GenerateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        _databaseContext.SaveChanges();
        return refreshToken;
    }

    public async Task<User?> GetUserByRefreshToken(RefreshTokenRequestDTO refreshTokenRequestDto)
    {
        return await _databaseContext.Users.FirstOrDefaultAsync(e =>
            e.RefreshToken == refreshTokenRequestDto.RefreshToken);
    }
}