using Microsoft.AspNetCore.Mvc;
using Status.Api.ViewModel;
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
        private readonly UserRepository userRepo;

        public UsersController(UserRepository userRepo)
        {
            this.userRepo = userRepo;
        }
                
        [HttpPost("v1/Add")]
        public async Task<IActionResult> Add(UserInputVM user)
        {
            // VERIFICA SE TODOS OS CAMPOS FORAM INFORMADOS
            if (String.IsNullOrEmpty(user.Nome) || 
                String.IsNullOrEmpty(user.Email) || 
                String.IsNullOrEmpty(user.Senha))
            {
                return BadRequest("Faltam campos obrigatórios");
            }

            // VERIFICA SE O E-MAIL JÁ ESTÁ CADASTRADO
            var checkUser = await userRepo.GetByEmailAsync(user.Email);
            if (checkUser != null)
            {
                return BadRequest("E-mail já cadastrado.");
            }

            await userRepo.Add(new Usuario
            {
                Nome = user.Nome,
                Email = user.Email,
                Senha = user.Senha
            });

            return Ok();
        }

    }
}
