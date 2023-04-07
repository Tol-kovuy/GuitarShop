using GuitarShop.BLL.UserService;
using GuitarShop.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GuitarShop.Controllers
{
    public class CabinetController : Controller
    {
        private readonly IUserService _userService;
        public CabinetController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Info()
        {
            var currentUser = HttpContext.User;
            var users = await _userService.GetAllAsync();
            var info = users.FirstOrDefault(u => u.UserName == currentUser.Identity.Name);
            return View(info);
        }
    }
}
