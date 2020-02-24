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
using System.Threading.Tasks;

namespace Status.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserService svcUser;

        public AuthController(UserService svcUser)
        {
            this.svcUser = svcUser;
        }

        [HttpGet]
        public IActionResult SignIn() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(string? returnUrl, SignInVM dados)
        {
            if (!ModelState.IsValid)
            {
                return View(dados);
            }

            if (!await LoginAsync(dados.Email, dados.Senha))
            {
                ModelState.AddModelError("","E-mail ou senha inválido.");
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
            var user = await svcUser.CheckAuthenticationAsync(email, password);
            if (user == null)
            {
                return false;
            }
            var claims = new List<Claim>()
            {
                new Claim("id", user.Id.ToString()),
                new Claim("email", email)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
                );

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal, new AuthenticationProperties
            {
                IsPersistent = true
            });

            return true;
        }
    }
}
