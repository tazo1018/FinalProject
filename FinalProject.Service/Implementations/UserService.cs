using AutoMapper;
using FinalProject.Contracts.Users;
using FinalProject.Models.Users;
using Microsoft.AspNetCore.Http;

namespace FinalProject.Service.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor )
    {
        _userRepository = userRepository;
        _mapper = MappingInitializer.Initialize();

    }

    public async Task BlockUserAsync(string id)
    {
        var row = await _userRepository.GetSingleUserAsync(id);
        if (row == null)
        {
            throw new Exception("User not found");
        }

        row.IsBlocked = true;

        _userRepository.UpdateUser(row);
        await _userRepository.Save();
    }

    public async Task UnblockUserAsync(string id)
    {
        var row = await _userRepository.GetSingleUserAsync(id);
        if (row == null)
        {
            throw new Exception("User not found");
        }

        row.IsBlocked = false;

        _userRepository.UpdateUser(row);
        await _userRepository.Save();
    }

    public async Task DeleteUserAsync(string id)
    {
        await _userRepository.DeleteUserAsync(id);
        await _userRepository.Save();
    }

    public async Task<List<UserForGettingDTO>> GetAllUsersAsync()
    {
        var rows = await _userRepository.GetAllUsersAsync();
        return _mapper.Map<List<UserForGettingDTO>>(rows);
    }

    public async Task<UserForGettingDTO> GetSingleUserAsync(string id)
    {
        var row = await _userRepository.GetSingleUserAsync(id);
        return _mapper.Map<UserForGettingDTO>(row);
    }

    public async Task UpdateUserAsync(UserForUpdatingDTO user)
    {
        var row = await _userRepository.GetSingleUserAsync(user.Id);
        if (row == null)
        {
            throw new Exception("User not found");
        }

        row.FirstName = user.FirstName;
        row.LastName = user.LastName;
        row.Email = user.Email;
        row.PhoneNumber = user.PhoneNumber;

        _userRepository.UpdateUser(row);
        await _userRepository.Save();
    }
}
