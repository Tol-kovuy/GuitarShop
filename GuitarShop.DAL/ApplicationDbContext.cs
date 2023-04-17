using GuitarShop.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace GuitarShop.DAL;

public class ApplicationDbContext : DbContext
{
    public DbSet<UserEntity> UserEntities { get; set; }
    public DbSet<ProductEntity> ProductEntities { get; set; }
    public DbSet<CartEntity> CartEntities { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=GuitarShopDb;Trusted_Connection=True;Encrypt=False;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>()
            .HasOne(user => user.CartEntity)
            .WithOne(cart => cart.UserEntity)
            .HasForeignKey<UserEntity>(user => user.CartEntityId);

        modelBuilder.Entity<CartEntity>()
            .HasOne(cart => cart.UserEntity)
            .WithOne(user => user.CartEntity)
            .HasForeignKey<CartEntity>(cart => cart.UserEntityId);

        modelBuilder.Entity<CartEntity>()
            .HasMany(cart => cart.ProductEntity);
    }
}