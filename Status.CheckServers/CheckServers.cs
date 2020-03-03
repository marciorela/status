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

        public void ThreadTestPort(object data)
        {
            var server = (PortStatusVM)data;

            _logger.LogInformation($"{DateTime.Now}: Verificando {server.Host}, porta {server.PortNumber}...");

            using (TcpClient tcpClient = new TcpClient())
            {
                // INDIFERENTE
                // TODO: verificar outro modo para definir timeout
                tcpClient.SendTimeout = 10;

                var timeStart = DateTime.Now;
                try
                {
                    tcpClient.Connect(server.Host, server.PortNumber);
                    var totalMS = (DateTime.Now - timeStart).TotalMilliseconds;
                    //await tcpClient.ConnectAsync(server.Host, server.Port);

                    svcServer.LogStatus(new PortCheckedVM { PortId = server.PortId, Status = true, TimeMS = totalMS });
                    tcpClient.Close();

                    _logger.LogInformation($"{DateTime.Now}: Serviço {server.Host}:{server.PortNumber} disponível.");

                }
                catch (Exception e)
                {
                    var totalMS = (DateTime.Now - timeStart).TotalMilliseconds;

                    svcServer.LogStatus(new PortCheckedVM { PortId = server.PortId, Status = false, TimeMS = totalMS, Obs = e.Message });
                    _logger.LogWarning($"{DateTime.Now}: Não foi possível acessar o serviço {server.Host}:{server.PortNumber}.");
                }

                //                    server.LastChecked = DateTime.Now;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            IEnumerable<PortStatusVM> listServer = null;
            var lastReadList = DateTime.MinValue;

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if ((DateTime.Now - lastReadList).TotalSeconds > 60)
                    {
                        listServer = await svcServer.ListAllPortsAsync();
                        lastReadList = DateTime.Now;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError($"Erro buscando lista...\n{e.Message}");
                }

                foreach (var server in listServer)
                {
                    if (server.Active && (DateTime.Now - server.LastChecked).TotalSeconds >= server.CheckInterval)
                    {
                        Thread t = new Thread(ThreadTestPort);
                        t.Start(server);
                        server.LastChecked = DateTime.Now;
                    }
                }

                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
