using GuitarShop.DAL.Entities;

namespace GuitarShop.DAL.Repositories;

public class UserRepository : IBaseRepository<UserEntity>
{
    private readonly ApplicationDbContext _context;

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

    public async Task UpdateAsync(UserEntity user)
    {
        _context.UserEntities.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(UserEntity user)
    {
        _context.UserEntities.Remove(user);
        await _context.SaveChangesAsync();
    }
}
