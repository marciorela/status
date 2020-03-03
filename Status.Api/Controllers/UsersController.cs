using Microsoft.AspNetCore.Mvc;
using MR.String;
using Status.Domain.ViewModels;
using Status.Data.Repositories;
using Status.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Api.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class UsersController : ControllerBase
    {
        private readonly UserRepository _userRepo;

        public UsersController(UserRepository userRepo)
        {
            this._userRepo = userRepo;
        }
                
        [HttpPost("v1/Add")]
        public async Task<IActionResult> Add(UserInputVM user)
        {
            // VERIFICA SE TODOS OS CAMPOS FORAM INFORMADOS
            if (String.IsNullOrEmpty(user.Nome) || 
                String.IsNullOrEmpty(user.Email) || 
                String.IsNullOrEmpty(user.Senha))
            {
                return BadRequest(new ReturnErrorVM { ErrorMessage = "Faltam campos obrigatórios" });
            }

            // VERIFICA SE O E-MAIL JÁ ESTÁ CADASTRADO
            var checkUser = await _userRepo.GetByEmailAsync(user.Email);
            if (checkUser != null)
            {
                return BadRequest(new ReturnErrorVM { ErrorMessage = "E-mail já cadastrado." });
            }

            var usuarioId = await _userRepo.Add(new Usuario
            {
                Nome = user.Nome,
                Email = user.Email,
                Senha = user.Senha.Encrypt()
            });

            return Ok(new ReturnIdVM { Id = usuarioId });
        }

        [HttpPost("v1/Authenticate")]
        public async Task<ReturnIdVM> Authenticate(SignInVM signin)
        {
            var user = await _userRepo.GetByEmailAsync(signin.Email) ;
            if (user == null || user != null && user.Senha != signin.Senha.Encrypt())
            {
                return null;
            }

            return new ReturnIdVM { Id = user.Id };
        }
    }
}
