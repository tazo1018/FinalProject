using FinalProject.Entities;

namespace FinalProject.Contracts.Users;

public interface IUserRepository : ISavable
{
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetSingleUserAsync(string id);
    void UpdateUser(User user);
    Task DeleteUserAsync(string id);
}
