using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Status.Domain.ViewModels;
using Status.Data.Repositories;
using Status.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Status.Service;

namespace Status.Api.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class ServersController : ControllerBase
    {
        private readonly ILogger<ServersController> logger;
        private readonly ServerRepository _serverRepo;

        public ServersController(ILogger<ServersController> logger, ServerRepository serverRepo)
        {
            this.logger = logger;
            this._serverRepo = serverRepo;
        }

        [HttpGet("v1/ListStatusByUser")]
        public async Task<IEnumerable<PortStatusVM>> List(Guid user)
        {
            return await _serverRepo.ListStatusByUserIdAsync(user);
        }

        [HttpGet("v1/ListAllPorts")]
        public async Task<IEnumerable<PortStatusVM>> ListAllPorts()
        {
            return await _serverRepo.ListAllPortsAsync();
        }

        [HttpGet("v1/ListByUser")]
        public async Task<IEnumerable<Servidor>> ListByUser(Guid user)
        {
            return await _serverRepo.ListByUserAsync(user);
        }

        [HttpGet("v1/GetByHost")]
        public async Task<Servidor> GetByHost(Guid userId, string host)
        {
            return await _serverRepo.GetByHostAsync(userId, host);
        }

        [HttpPost("v1/Add")]
        public async Task<IActionResult> Add(ServersVM server)
        {

            try
            {
                if (!await _serverRepo.ExistsAsync(server.UsuarioId, server.Host))
                {

                    //AcceptedAtAction

                    return Ok(new ReturnIdVM
                    {
                        Id = await _serverRepo.Add(new Servidor
                        {
                            UsuarioId = server.UsuarioId,
                            Nome = server.Nome,
                            Host = server.Host
                        })
                    });
                }
                else
                {
                    return BadRequest(new ReturnErrorVM { ErrorMessage = "Host já cadastrado." });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new ReturnErrorVM { ErrorMessage = $"Ocorreu um erro inesperado: {e.Message}" });
            }

        }

        [HttpPost("v1/Update")]
        public async Task<IActionResult> Edit(ServersVM server)
        {
            try
            {
                await _serverRepo.Update(new Servidor
                {
                    Id = server.Id,
                    Host = server.Host,
                    Nome = server.Nome,
                    UsuarioId = server.UsuarioId
                });

                return Ok(new ReturnIdVM { Id = server.Id });
            }
            catch (Exception e)
            {
                return BadRequest(new ReturnErrorVM { ErrorMessage = $"Ocorreu um erro inesperado: {e.Message}" });
            }
        }


    }
}
