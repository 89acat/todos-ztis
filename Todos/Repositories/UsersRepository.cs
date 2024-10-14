using Microsoft.EntityFrameworkCore;
using Todos.EF.Context;
using Todos.Models;

namespace Todos.Repositories;

public interface IUsersRepository
{
    Task<User?> GetUser(string userId);
    Task EditUser(string userId, User user);
}

public class UsersRepository : IUsersRepository
{
    private readonly TodosDbContext _context;
    
    public UsersRepository(TodosDbContext context)
    {
        _context = context;
    }
    
    public async Task<User?> GetUser(string userId)
    {
        var query = _context.Users.Where(x => x.Id == userId);
        return await query
            .Select(x => new User
            {
                Id = x.Id.ToString(),
                Email = x.Email
            })
            .FirstOrDefaultAsync();
    }
    
    public async Task EditUser(string userId, User user)
    {
        var userEntity = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (userEntity is null) throw new NotSupportedException();
        userEntity.Email = user.Email;
        
        await _context.SaveChangesAsync();
    }
}