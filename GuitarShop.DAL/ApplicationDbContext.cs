using GuitarShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GuitarShop.DAL;

public class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options
        )
        : base(options)
    {
        Database.EnsureCreated();
    }
}