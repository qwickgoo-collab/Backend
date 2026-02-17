using QwickGo.Core.Entities;

namespace QwickGo.Core.Interfaces;
public interface IUserRepository
{
    // Task <User?> GetByFirebaseUid(string uid);
    Task <User?> GetUserByEmail(string email);
    Task AddUser(User user);
    Task SaveChangesAsync();
}