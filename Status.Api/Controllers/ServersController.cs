using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Status.Api.ViewModel;
using Status.Data.Repositories;
using Status.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.Api.Controllers
{
    [ApiController]
    [Route("{controller}")]
    public class ServersController : ControllerBase
    {
        private readonly ILogger<ServersController> logger;
        private readonly ServerRepository serverRepo;

        public ServersController(ILogger<ServersController> logger, ServerRepository serverRepo)
        {
            this.logger = logger;
            this.serverRepo = serverRepo;
        }
        
        [HttpGet("v1/List")]
        public async Task<IEnumerable<Servidor>> List(Guid user)
        {
            return await serverRepo.ListByUserAsync(user);
        }

        [HttpPost("v1/Add")]
        public async Task<IActionResult> Add(ServersVM server)
        {

            try
            {
                await serverRepo.Add(new Servidor
                {
                    UsuarioId = server.UsuarioId,
                    Host = server.Host,
                    CheckInterval = server.CheckInterval
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message); // "Ocorreu um erro incluindo o servidor.");
            }

            return Ok();
        }
    }
}
