using Microsoft.AspNetCore.Mvc;
using Status.Data.Repositories;
using Status.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Status.UI.Controllers
{
    public class ServerController : Controller
    {
        private readonly ServerService serverService;
        private readonly AspNetUser aspNetUser;

        public ServerController(ServerService serverRepo, Service.AspNetUser aspNetUser)
        {
            this.serverService = serverRepo;
            this.aspNetUser = aspNetUser;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listServer = await serverService.ListByUserAsync(aspNetUser.Id);

            return View(listServer);
        }
    }
}
