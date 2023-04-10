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
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Info()
        {
            var currentUser = HttpContext.User;
            var users = await _userService.GetAllAsync();
            var info = users.FirstOrDefault(u => u.UserName == currentUser.Identity.Name);
            var collections = new User
            {
                Id = info.Id,
                UserName = info.UserName,
                FirstName = info.FirstName,
                LastName = info.LastName,
                Email = info.Email,
                Password = info.Password,
                Role = info.Role,
                Users = users,
            };
            return View(collections);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.DeleteAsync(id);
                ModelState.AddModelError("", response.Description);
            }
                
            return RedirectToAction("Info", "Cabinet");
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User model)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.CreateAsync(model);
                if (response.StatusCode == BLL.Enum.StatusCode.OK)
                {
                    return RedirectToAction("Info", "Cabinet");
                }
                ModelState.AddModelError("", response.Description);
            }
            return View(model);
        }
    }
}
