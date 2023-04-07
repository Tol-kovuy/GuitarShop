using AutoMapper;
using GuitarShop.BLL.Models;
using GuitarShop.BLL.UserService;
using GuitarShop.DAL;
using GuitarShop.DAL.Entities;
using GuitarShop.DAL.Repositories;
using GuitarShop.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GuitarShop.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public RegistrationController(
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = _mapper.Map<User>(model);
                await _userService.CreateAsync(newUser);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            var foundingUser = _userService.GetAll().FirstOrDefault(u => u.Id == id);
            if (foundingUser != null)
            {
                _userService.Delete(foundingUser.Id);
            }

            return RedirectToAction("Login", "Home");
        }
    }
}
