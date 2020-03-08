using Microsoft.EntityFrameworkCore;
using MR.String;
using Status.Domain.Entities;
using Status.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Data.Repositories
{
    public class UserRepository : BaseRepository<Usuario>
    {
        public UserRepository(AppDbContext ctx) : base(ctx)
        {
        }

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            return await _db.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<Guid> AddUserAsync(UsersVM user)
        {

            // VERIFICA SE TODOS OS CAMPOS FORAM INFORMADOS
            if (String.IsNullOrEmpty(user.Nome) ||
                String.IsNullOrEmpty(user.Email) ||
                String.IsNullOrEmpty(user.Senha))
            {
                throw new Exception("Faltam campos obrigatórios");
            }
                       
            // VERIFICA SE O E-MAIL JÁ ESTÁ CADASTRADO
            var checkUser = await GetByEmailAsync(user.Email);
            if (checkUser != null)
            {
                throw new Exception("E-mail já cadastrado.");
            }

            var usuarioId = await Add(new Usuario
            {
                Nome = user.Nome,
                Email = user.Email,
                Senha = user.Senha.Encrypt()
            });

            return usuarioId;
        }
    }
}
