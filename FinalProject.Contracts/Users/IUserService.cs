using FinalProject.Models.Users;

namespace FinalProject.Contracts.Users;

public interface IUserService
{
    Task<List<UserForGettingDTO>> GetAllUsersAsync();
    Task<UserForGettingDTO> GetSingleUserAsync(string id);
    Task UpdateUserAsync(UserForUpdatingDTO user);
    Task BlockUserAsync(string id);
    Task UnblockUserAsync(string id);
    Task DeleteUserAsync(string id);
}
