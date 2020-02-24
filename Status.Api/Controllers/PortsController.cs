using Microsoft.AspNetCore.Mvc;
using Status.Domain.ViewModels;
using Status.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Api.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class PortsController : ControllerBase
    {
        private readonly ServerRepository _repoServer;
        private readonly PortRepository _repoPort;

        public PortsController(ServerRepository repoServer, PortRepository repoPort)
        {
            _repoServer = repoServer;
            _repoPort   = repoPort;
        }

        [HttpPost("v1/Add")]
        public async Task<IActionResult> Add(PortAddVM port)
        {
            try
            {
                if (!await _repoServer.ExistsAsync(port.ServerId))
                {
                    throw new Exception("Servidor não encontrado.");
                }

                if (await _repoPort.ExistsAsync(port.ServerId, port.Port))
                {
                    throw new Exception("Porta já existe.");
                }

                return (Ok(new ReturnIdVM { Id = await _repoServer.AddPort(port.ServerId, port.Port) }));

            }
            catch (Exception e)
            {
                return BadRequest(new ReturnErrorVM { ErrorMessage = e.Message });
            }
        }
    }
}

