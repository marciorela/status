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
    public class LogCheckedRepository
    {
        private readonly AppDbContext ctx;
        private readonly DbSet<LogChecked> _db;

        public LogCheckedRepository(AppDbContext ctx)
        {
            this.ctx = ctx;
            _db = ctx.Set<LogChecked>();
        }

        public async Task<Guid> AddCheckedAsync(LogChecked logChecked)
        {
            await _db.AddAsync(logChecked);
            await ctx.SaveChangesAsync();

            return logChecked.Id;
        }
    }
}
