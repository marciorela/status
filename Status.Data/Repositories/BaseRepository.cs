using Status.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Data.Repositories
{
    public class BaseRepository
    {
        protected readonly AppDbContext ctx;

        public BaseRepository(AppDbContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task Add(BaseEntity entity)
        {
            if (entity.Id == null)
            {
                entity.Id = Guid.NewGuid();
            }

            await ctx.AddAsync(entity);
            await ctx.SaveChangesAsync();
        }

        public async Task Update(BaseEntity entity)
        {
            ctx.Update(entity);
            await ctx.SaveChangesAsync();

        }

        public async Task<BaseEntity> GetById(Guid Id)
        {
            return await ctx.FindAsync<BaseEntity>(Id);
        }
    }
}
