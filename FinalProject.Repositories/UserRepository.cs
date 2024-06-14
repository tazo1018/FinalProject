using FinalProject.Contracts.Users;
using FinalProject.Data;
using FinalProject.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FinalProject.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task AddUserAsync(User user)
    {
        if (user != null)
        {
            await _context.Users.AddAsync(user);
        }
    }

    public async Task DeleteUserAsync(string id)
    {
        if (!string.IsNullOrWhiteSpace(id))
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Id == id);
            _context.Users.Remove(user);
        }
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<List<User>> GetAllUsersAsync(Expression<Func<User, bool>> filter)
    {
        return await _context.Users
            .Where(filter)
            .ToListAsync();
    }

    public async Task<User> GetSingleUserAsync(Expression<Func<User, bool>> filter)
    {
        return await _context.Users.FirstOrDefaultAsync(filter);
    }

    public async Task<User> GetSingleUserAsync(string id)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task Save() => await _context.SaveChangesAsync();

    public void UpdateUser(User user)
    {

        _context.Users.Update(user);


    }
}
