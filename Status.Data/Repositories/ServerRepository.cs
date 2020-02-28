using Microsoft.EntityFrameworkCore;
using Status.Domain.Entities;
using Status.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Data.Repositories
{
    public class ServerRepository : BaseRepository<Servidor>
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

        public async Task<IEnumerable<ServersAllVM>> ListAllServersAsync()
        {
            var list = await ctx.PortasServidor
                .Include(s => s.Servidor)
                .Where(p => p.Active)
                .ToListAsync();

            // ERRO 
            //var list = await ctx.Servidores
            //                .Include(p => p.Portas.Where(x => x.Active))
            //                .ToListAsync();

            var resultList = new List<ServersAllVM>();

            foreach (var item in list)
            {
                resultList.Add(
                    new ServersAllVM
                    {
                        UserId = item.Servidor.UsuarioId,
                        ServerId = item.ServidorId,
                        Host = item.Servidor.Host,
                        CheckInterval = item.CheckInterval,
                        Port = item.Numero,
                        PortId = item.Id
                    }
                );
            }

            /*
                        foreach (var item in list)
                        {
                            foreach (var port in item.Portas)
                            {
                                resultList.Add(
                                    new ServersAllVM
                                    {
                                        UserId = item.UsuarioId,
                                        ServerId = item.Id,
                                        Host = item.Host,
                                        CheckInterval = port.CheckInterval,
                                        Port = port.Numero,
                                        PortId = port.Id
                                    }
                                );
                            }
                        }
            */

            return resultList;
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

        public async Task<Guid> AddPort(Guid serverId, int portNumber, int checkInterval)
        {
            var port = new Porta
            {
                Numero = portNumber,
                ServidorId = serverId,
                CheckInterval = checkInterval,
                Active = true
            };

            await _portRepo.Add(port);

            return port.Id;
        }
    }
}
