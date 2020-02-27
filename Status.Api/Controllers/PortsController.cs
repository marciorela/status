using Microsoft.AspNetCore.Mvc;
using Status.Domain.ViewModels;
using Status.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Status.Domain.Entities;

namespace Status.Api.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class PortsController : ControllerBase
    {
        private readonly ServerRepository _repoServer;
        private readonly PortRepository _repoPort;
        private readonly LogCheckedRepository repoLog;

        public PortsController(ServerRepository repoServer, PortRepository repoPort, LogCheckedRepository repoLog)
        {
            _repoServer = repoServer;
            _repoPort = repoPort;
            this.repoLog = repoLog;
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

                return (Ok(new ReturnIdVM { Id = await _repoServer.AddPort(port.ServerId, port.Port, port.CheckInterval) }));

            }
            catch (Exception e)
            {
                return BadRequest(new ReturnErrorVM { ErrorMessage = e.Message });
            }
        }

        [HttpPost("v1/Checked")]
        public async Task<IActionResult> Checked(PortCheckedVM portChecked)
        {

            if (!await _repoPort.ExistsAsync(portChecked.PortId))
            {
                return BadRequest(new ReturnErrorVM { ErrorMessage = "Porta não encontrada." });
            }

            var porta = await _repoPort.GetByIdAsync(portChecked.PortId);

            var logChecked = new LogChecked
            {
                PortId = portChecked.PortId,
                PortNumber = porta.Numero,
                Obs = portChecked.Obs,
                Status = portChecked.Status,
                DateTimeChecked = portChecked.DataChecked
            };

            return Ok(
                new ReturnIdVM {
                    Id = await repoLog.AddCheckedAsync(logChecked) 
                });
        }

    }
}

