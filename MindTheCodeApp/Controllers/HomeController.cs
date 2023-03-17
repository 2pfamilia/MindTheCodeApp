using AppCore.IRepositories;
using AppCore.Models;
using AppCore.Models.DTOs;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AppCore.Controllers
{
    [Route("")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            //  Return HomeDTO with all the needed data
            /*
                HomeDTO needs to have three lists with best sellers,
                new arrivals, and authors

                The business logic will need to be in a service.
            */

            var dto = new HomeDTO();

            return View("Views/Home/Index.cshtml", dto);
        }
    }
}
