using MR.String;
using Status.Data.Repositories;
using Status.Domain.Entities;
using Status.Domain.ViewModels;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Status.Service
{
    public class UserService : BaseService
    {    
        public UserService()
        {
        }

        public async Task<ReturnIdVM> CheckAuthenticationAsync(string email, string password)
        {
            return await PostAsync<ReturnIdVM>("/users/v1/authenticate/", new SignInVM { Email = email, Senha = password });
        }

        public async Task<Guid> AddUserAsync(UsersVM user)
        {
            var id = await PostAsync<ReturnIdVM>("/users/v1/Add/", user);

            return id.Id;
        }
    }
}
