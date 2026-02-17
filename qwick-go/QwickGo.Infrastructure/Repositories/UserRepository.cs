using QwickGo.Core.Entities;
using QwickGo.Core.Interfaces;
using QwickGo.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace QwickGo.Infrastructure.Repositories;
public class UserRepository : IUserRepository
{
    private readonly QwickGoDbContext _dbContext;

    public UserRepository(QwickGoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // public async Task<User?> GetByFirebaseUid(string uid)
    // {
        
    // }

    public async Task<User?> GetUserByEmail(string email)
    {
        User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        return user;
    }

    public async Task AddUser(User user)
    {
        _dbContext.Users.Add(user);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}