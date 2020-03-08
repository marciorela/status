using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MR.String;
using Status.Domain.ViewModels;
using Status.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Status.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserService userService;
        private readonly AspNetUser aspNetUser;

        public AuthController(UserService userService, AspNetUser aspNetUser)
        {
            this.userService = userService;
            this.aspNetUser = aspNetUser;
        }

        [HttpGet]
        public IActionResult SignIn() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(string? returnUrl, SignInVM dados)
        {
            if (!await LoginAsync(dados.Email, dados.Senha))
            {
                ModelState.AddModelError("", "E-mail ou senha inválido.");
            }

            return Redirect(returnUrl ?? "/");
        }

        [HttpGet]
        public async Task<IActionResult> LogOff()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            var user = await userService.CheckAuthenticationAsync(email, password);
            if (user == null)
            {
                return false;
            }

            await aspNetUser.Authenticate(user.Id, email);

            return true;
        }
    }
}

