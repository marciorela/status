using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Data.Repositories
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(AppDbContext ctx) : base(ctx)
        {
        }
    }
}
