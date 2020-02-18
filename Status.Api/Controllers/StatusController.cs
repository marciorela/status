using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Status.Api.ResultAPI;
using Status.Data.Repositories;
using Status.Domain.Entities;

namespace Status.Api.Controllers
{
    [ApiController]
    [Route("{controller}/api")]
    public class StatusController : ControllerBase
    {
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        private readonly ILogger<StatusController> _logger;
        private readonly ServerRepository _serverRepo;

        public StatusController(ILogger<StatusController> logger, ServerRepository serverRepo)
        {
            _logger = logger;
            _serverRepo = serverRepo;
        }

        [HttpGet("GetServers")]
        public async Task<IEnumerable<Servidor>> GetServers(Guid idUser)
        {
            return await _serverRepo.ListByUserAsync(idUser);
        }

        //public IEnumerable<WeatherForecast> GetServers()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
    }
}
