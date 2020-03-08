using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Status.Service;
using Status.UI.Models;

namespace Status.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ServerService _serverService;
        private readonly AspNetUser aspNetUser;

        public HomeController(ILogger<HomeController> logger, ServerService serverService, AspNetUser aspNetUser)
        {
            _logger = logger;
            this._serverService = serverService;
            this.aspNetUser = aspNetUser;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var list = await _serverService.ListPortsByServerAsync(aspNetUser.Id);

            return View(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
