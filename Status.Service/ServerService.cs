using Status.Data.Repositories;
using Status.Domain.Entities;
using Status.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Policy;
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

        public async Task<IEnumerable<Servidor>> ListByUserAsync(Guid user)
        {
            return await GetAsync<IEnumerable<Servidor>>("/Servers/v1/ListByUser", $"user={user}");
        }

        public async Task<IEnumerable<PortStatusVM>> ListStatusByUserAsync(Guid user)
        {
            return await GetAsync<IEnumerable<PortStatusVM>>("/Servers/v1/ListStatusByUser", $"user={user}");
        }

        public async Task<IEnumerable<ServerPortsVM>> ListPortsByServerAsync(Guid userId)
        {
            var resultlist = new List<ServerPortsVM>();
            var lastHost = "";
            var list = await ListStatusByUserAsync(userId);
            if (list != null)
            {
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
            }

            return resultlist;
        }

        private async Task<Guid> AddServer(Guid UserId, ServersVM server)
        {
            var id = await PostAsync<ReturnIdVM>("/Servers/v1/Add", new ServersVM
            {
                Host = server.Host,
                Nome = server.Nome,
                UsuarioId = UserId
            });

            return id.Id;
        }

        private async Task<Guid> EditServer(Guid userId, ServersVM server)
        {
            var id = await PostAsync<ReturnIdVM>("/Servers/v1/Update", new ServersVM
            {
                Id = server.Id,
                Host = server.Host,
                Nome = server.Nome,
                UsuarioId = userId
            });

            return id.Id;
        }

        public async Task<Servidor> GetByHost(Guid userId, string host)
        {
            return await GetAsync<Servidor>("/Servers/v1/GetByHost", $"user={userId}&host={host}");
        }

        public async Task<Porta> GetPort(Guid idServer, int portNumber)
        {
            return await GetAsync<Porta>("/Servers/v1/GetPort", $"server={idServer}&port={portNumber}");
        }

        public async Task<Guid> AddOrEditServer(ServerEditVM server)
        {
            Guid id;

            var checkServer = await GetByHost(server.UserId, server.Host);
            if (checkServer == null)
            {
                id = await AddServer(server.UserId, new ServersVM
                {
                    Host = server.Host,
                    Nome = server.Nome,
                    UsuarioId = server.UserId
                });

            }
            else
            {
                id = await EditServer(server.UserId, new ServersVM
                {
                    Id = checkServer.Id,
                    Host = server.Host,
                    Nome = server.Nome,
                    UsuarioId = server.UserId
                });
            }

            var ports = server.Portas.Split(',');
            foreach (var portNumber in ports)
            {
                var port = GetPort(id, Convert.ToInt32(portNumber));
                if (port == null)
                {
                    await PostAsync<ReturnIdVM>("/Ports/v1/Add", new PortAddVM
                    {
                        ServerId = id,
                        Port = Convert.ToInt32(port)
                    });
                }
            }

            return id;
        }
    }
}
