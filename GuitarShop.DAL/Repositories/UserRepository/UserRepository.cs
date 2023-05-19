using GuitarShop.DAL.Entities;

namespace GuitarShop.DAL.Repositories.UserRepository;

public class UserRepository : IBaseRepository<User>
{
    private readonly ApplicationDbContext _context;

    public UserRepository(
        ApplicationDbContext applicationDbContext
        )
    {
        _context = applicationDbContext;
    }

    public async Task CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public IQueryable<User> GetAll()
    {
        return _context.Users;
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}
