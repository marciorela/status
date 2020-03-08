using Microsoft.AspNetCore.Mvc;
using MR.String;
using Status.Domain.ViewModels;
using Status.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService userService;

        public UserController(UserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult New() => View();

        [HttpPost]
        public async Task<IActionResult> New(UserNewVM user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            await userService.AddUserAsync(new UsersVM
            {
                Email = user.Email,
                Nome = user.Nome,
                Senha = user.Senha
            });

            return RedirectToAction("Index", "Home");
        }

    }
}
