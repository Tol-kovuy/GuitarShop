using GuitarShop;
using GuitarShop.BLL.AccountService;
using GuitarShop.BLL.CartService;
using GuitarShop.BLL.CategoryService;
using GuitarShop.BLL.ProductService;
using GuitarShop.BLL.UserService;
using GuitarShop.Controllers;
using GuitarShop.DAL;
using GuitarShop.DAL.Entities;
using GuitarShop.DAL.Repositories;
using GuitarShop.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        var connectionString = builder.Configuration.GetConnectionString("SQLLocalConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            options.UseSqlServer(connectionString);

        });
        builder.Services.AddAutoMapper(typeof(MapperProfile));
        builder.Services.AddScoped<MapperProfile>();
        builder.Services.AddScoped<IBaseRepository<User>, UserRepository>();
        builder.Services.AddScoped<IBaseRepository<Product>, ProductRepository>();
        builder.Services.AddScoped<IBaseRepository<Category>, CategoryRepository>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<ICartRepository, CartRepository>();
        builder.Services.AddScoped<ICartService, CartService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<MappingImageFile>();
        builder.Services.AddScoped<GuitarShop.Controllers.ControllerBase>();
        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
        {
            options.LoginPath = new PathString("/Authentication/Login");
            options.AccessDeniedPath = new PathString("/Authentication/Login");
        });

        var app = builder.Build();


        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");

            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}