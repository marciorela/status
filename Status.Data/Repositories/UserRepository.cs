using Microsoft.EntityFrameworkCore;
using Status.Domain.Entities;
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
    }
}
