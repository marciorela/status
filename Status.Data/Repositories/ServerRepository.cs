using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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
        private readonly PortRepository _portRepo;

        public ServerRepository(AppDbContext ctx) : base(ctx)
        {
        }

        public ServerRepository(AppDbContext ctx, PortRepository portRepo) : base(ctx)
        {
            _portRepo = portRepo;
        }

        public async Task<IEnumerable<PortStatusVM>> ListStatusByUserIdAsync(Guid? userId = null)
        {
            var textSQL = $@"
                select
                  p.id as portid, p.numero as portnumber, s.host, p.active, p.checkinterval, l.datetimechecked as lastchecked, l.status
                from usuarios u
                inner join servidores s on
                  s.usuarioid = u.id
                left join portasservidor p on
                  p.servidorid = s.id
                left join (select l1.id, l1.portid, l1.datetimechecked, l1.status
                           from logschecked l1
                           where
                             l1.id = (select l2.id from logschecked l2 where l2.portid = l1.portid order by datetimechecked desc limit 1)
                          ) l on
                  l.portid = p.id
                ##
                order by s.host, p.numero
            ";

            if (userId != null)
            {
                textSQL = textSQL.Replace("##", $@"
                where
                  u.id = '{userId}'
                ");
            }
            else
            {
                textSQL = textSQL.Replace("##", "");
            };

            var resultList = await ctx.Database.GetDbConnection().QueryAsync<PortStatusVM>(textSQL);

            return resultList;
        }

        public async Task<IEnumerable<Servidor>> ListByUserAsync(Guid userId)
        {
            return await ctx.Servidores.Where(s => s.UsuarioId == userId).OrderBy(s => s.Host).ToListAsync();
        }

        public async Task<Servidor> GetByHostAsync(Guid usuarioId, string host)
        {
            return await ctx.Servidores.Where(s => s.UsuarioId == usuarioId && s.Host == host).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PortStatusVM>> ListAllPortsAsync()
        {
            var resultList = await ListStatusByUserIdAsync();

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
