using Status.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Service
{
    public class ServerService : BaseService
    {
        
        public async Task<IEnumerable<ServersAllVM>> ListAllAsync()
        {
            return await GetAsync<IEnumerable<ServersAllVM>>("/Servers/v1/ListAll", "");
        }

    }
}
