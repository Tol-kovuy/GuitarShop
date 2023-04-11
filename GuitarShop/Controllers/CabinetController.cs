using AutoMapper;
using Azure;
using GuitarShop.BLL.AccountService;
using GuitarShop.BLL.Models;
using GuitarShop.BLL.UserService;
using Microsoft.AspNetCore.Mvc;

namespace GuitarShop.Controllers
{
    public class CabinetController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public CabinetController(
            IUserService userService,
            IAccountService accountService,
            IMapper mapper
            )
        {
            _userService = userService;
            _accountService = accountService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var currentUser = HttpContext.User;
            var users = await _userService.GetAllAsync();
            var info = users.FirstOrDefault(u => u.UserName == currentUser.Identity.Name);
            return View(info);
        }
    }
}
