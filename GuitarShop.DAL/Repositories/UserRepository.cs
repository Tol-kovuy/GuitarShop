using GuitarShop.DAL.Entities;

namespace GuitarShop.DAL.Repositories;

public class UserRepository : IBaseRepository<UserEntity>
{
    private ApplicationDbContext _context;

    public UserRepository(
        ApplicationDbContext applicationDbContext
        )
    {
        _context = applicationDbContext;
    }

    public async Task CreateAsync(UserEntity user)
    {
        await _context.UserEntities.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public IQueryable<UserEntity> GetAll()
    {
        return _context.UserEntities;
    }

    public async Task<UserEntity> UpdateAsync(UserEntity user)
    {
        _context.UserEntities.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteAsync(UserEntity user)
    {
        _context.UserEntities.Remove(user);
        await _context.SaveChangesAsync();
    }
}
