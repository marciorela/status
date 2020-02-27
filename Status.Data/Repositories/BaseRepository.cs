using Microsoft.EntityFrameworkCore;
using Status.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Data.Repositories
{
    public class BaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly AppDbContext ctx;
        protected readonly DbSet<TEntity> _db;

        public BaseRepository(AppDbContext ctx)
        {
            this.ctx = ctx;
            _db = ctx.Set<TEntity>();
        }

        public async Task<Guid> Add(BaseEntity entity)
        {
            if (entity.Id == null)
            {
                entity.Id = Guid.NewGuid();
            }

            await ctx.AddAsync(entity);
            await ctx.SaveChangesAsync();

            return entity.Id;
        }

        public async Task Update(BaseEntity entity)
        {
            ctx.Update(entity);
            await ctx.SaveChangesAsync();

        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _db.FindAsync(id);
        }
    }
}
