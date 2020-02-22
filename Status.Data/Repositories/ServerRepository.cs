using Microsoft.EntityFrameworkCore;
using Status.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Data.Repositories
{
    public class ServerRepository : BaseRepository
    {
        private readonly UserRepository _userRepo;

        public ServerRepository(AppDbContext ctx) : base(ctx)
        {
        }

        public ServerRepository(AppDbContext ctx, UserRepository userRepo) : base(ctx)
        {
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<Servidor>> ListByUserAsync(Guid userId)
        {
            return await ctx.Servidores.Where(s => s.UsuarioId == userId).ToListAsync();
        }

        public async Task<Servidor> GetByHostAsync(Guid usuarioId, string host)
        {
            return await ctx.Servidores.Where(s => s.UsuarioId == usuarioId && s.Host == host).FirstOrDefaultAsync();
        }

        public async Task<bool> Exists(Guid usuarioId, string host)
        {
            var servidor = await GetByHostAsync(usuarioId, host);

            return (servidor != null);

        }
    }
}
