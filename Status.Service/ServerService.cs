using Status.Data.Repositories;
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
        private readonly UserService userService;

        public ServerService()
        {

        }

        public ServerService(UserService userService)
        {
            this.userService = userService;
        }

        public async Task<IEnumerable<PortStatusVM>> ListAllPortsAsync()
        {
            return await GetAsync<IEnumerable<PortStatusVM>>("/Servers/v1/ListAllPorts", "");
        }

        public async Task<ReturnIdVM> LogStatusAsync(PortCheckedVM portCheckedVM)
        {
            return await PostAsync<ReturnIdVM>("/Ports/v1/Checked", portCheckedVM);
        }

        public ReturnIdVM LogStatus(PortCheckedVM portCheckedVM)
        {
            return Task.Run(async () => await LogStatusAsync(portCheckedVM)).Result;
        }

        public async Task<IEnumerable<PortStatusVM>> ListByUser(Guid user)
        {
            return await GetAsync<IEnumerable<PortStatusVM>>("/Servers/v1/ListByUser", $"user={user}");
        }

        public async Task<IEnumerable<ServerPortsVM>> ListPortsByServerAsync(Guid userId)
        {
            var resultlist = new List<ServerPortsVM>();
            var lastHost = "";
            var list = await ListByUser(userId);
            foreach (var item in list)
            {
                if (lastHost != item.Host)
                {
                    resultlist.Add(new ServerPortsVM
                    {
                        Host = item.Host                        
                    });
                }
                lastHost = item.Host;

                resultlist.Last().Portas.Add(new ServerPortStatusVM
                {
                    Id = item.PortId,
                    Numero = item.PortNumber,
                    Status = item.Status                    
                });

                if (item.Status)
                {
                    resultlist.Last().PortsOk++;
                }
                else
                {
                    resultlist.Last().PortsError++;
                }
            }

            return resultlist;
        }
    }
}
