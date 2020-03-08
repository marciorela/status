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
using Status.Service;

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
        public async Task<IActionResult> Add(UsersVM user)
        {
            try
            {
                return Ok(new ReturnIdVM { Id = await _userRepo.AddUserAsync(user) });
            }
            catch (Exception e)
            {
                return BadRequest(new ReturnErrorVM { ErrorMessage = e.Message });
            }
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
