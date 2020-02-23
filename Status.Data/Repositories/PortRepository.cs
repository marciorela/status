using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Data.Repositories
{
    public class PortRepository : BaseRepository
    {
        public PortRepository(AppDbContext ctx) : base(ctx)
        {
        }

        public async Task<bool> ExistsAsync(Guid serverId, int port)
        {
            return await ctx.PortasServidor.AnyAsync(p => p.ServidorId == serverId && p.Numero == port);
        }
    }
}
