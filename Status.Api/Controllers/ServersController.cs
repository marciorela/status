﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly ServerRepository _serverRepo;

        public ServersController(ILogger<ServersController> logger, ServerRepository serverRepo)
        {
            this.logger = logger;
            this._serverRepo = serverRepo;
        }

        [HttpGet("v1/List")]
        public async Task<IEnumerable<Servidor>> List(Guid user)
        {
            return await _serverRepo.ListByUserAsync(user);
        }

        [HttpPost("v1/Add")]
        public async Task<IActionResult> Add(ServersVM server)
        {

            try
            {
                if (!await _serverRepo.Exists(server.UsuarioId, server.Host))
                {

                    var novoServidor = new Servidor
                    {
                        UsuarioId = server.UsuarioId,
                        Host = server.Host,
                        CheckInterval = server.CheckInterval
                    };

                    await _serverRepo.Add(novoServidor);

                    return Ok(new ReturnIdVM { Id = novoServidor.Id });
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
    }
}