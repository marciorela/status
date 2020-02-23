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
        private readonly PortRepository _portRepo;

        public ServerRepository(AppDbContext ctx) : base(ctx)
        {
        }

        public ServerRepository(AppDbContext ctx, UserRepository userRepo, PortRepository portRepo) : base(ctx)
        {
            _userRepo = userRepo;
            _portRepo = portRepo;
        }

        public async Task<IEnumerable<Servidor>> ListByUserAsync(Guid userId)
        {
            return await ctx.Servidores.Where(s => s.UsuarioId == userId).ToListAsync();
        }

        public async Task<Servidor> GetByHostAsync(Guid usuarioId, string host)
        {
            return await ctx.Servidores.Where(s => s.UsuarioId == usuarioId && s.Host == host).FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsAsync(Guid usuarioId, string host)
        {
            return await (ctx.Servidores.AnyAsync(c => c.UsuarioId == usuarioId && c.Host == host));

            //var servidor = await GetByHostAsync(usuarioId, host);
            //return (servidor != null);
        }

        public async Task<bool> ExistsAsync(Guid serverId)
        {
            return await (ctx.Servidores.AnyAsync(c => c.Id == serverId));
        }

        public async Task<Guid> AddPort(Guid serverId, int portNumber)
        {
            var port = new Porta
            {
                Numero = portNumber,
                ServidorId = serverId
            };

            await _portRepo.Add(port);

            return port.Id;
        }
    }
}
