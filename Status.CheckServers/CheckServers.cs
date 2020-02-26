using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Status.Domain.ViewModels;
using Status.Service;

namespace Status.CheckServers
{
    public class CheckServers : BackgroundService
    {
        private readonly ILogger<CheckServers> _logger;
        private readonly ServerService svcServer;

        public CheckServers(ILogger<CheckServers> logger, IOptions<ServerService> svcServer)
        {
            _logger = logger;
            this.svcServer = svcServer.Value;
        }

        private void TestPort(ServersAllVM server)
        {
            _logger.LogInformation($"{DateTime.Now}: Verificando {server.Host}, porta {server.Port}...");

            using (TcpClient tcpClient = new TcpClient())
            {
                tcpClient.SendTimeout = 10;

                try
                {
                    tcpClient.Connect(server.Host, server.Port);
                    //                        await tcpClient.ConnectAsync(server.Host, server.Port);
                    _logger.LogInformation($"{DateTime.Now}: Serviço {server.Host}:{server.Port} disponível.");

                    tcpClient.Close();
                }
                catch (Exception e)
                {
                    _logger.LogWarning($"{DateTime.Now}: Não foi possível acessar o serviço {server.Host}:{server.Port}.");
                }

                //                    server.LastChecked = DateTime.Now;
            }
        }

        public void ThreadTestPort(object data)
        {
            var server = (ServersAllVM)data;

            TestPort(server);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IEnumerable<ServersAllVM> listServer = null;
            var lastReadList = DateTime.MinValue;

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if ((DateTime.Now - lastReadList).TotalSeconds > 300)
                    {
                        listServer = await svcServer.ListAllAsync();
                        lastReadList = DateTime.Now;
                    }

                    /*
                    Parallel.ForEach(listServer, (server) =>
                    {
                        TestPort(server);
                    });
                    */

                    foreach (var server in listServer)
                    {
                        if ((DateTime.Now - server.LastChecked).TotalSeconds >= server.CheckInterval)
                        {
                            Thread t = new Thread(ThreadTestPort);
                            t.Start(server);
                            server.LastChecked = DateTime.Now;
                        }
                    }

                }
                catch (Exception e)
                {
                    _logger.LogError($"Erro buscando lista...\n{e.Message}");
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
