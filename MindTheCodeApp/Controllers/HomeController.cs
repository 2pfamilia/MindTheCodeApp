using AppCore.IRepositories;
using AppCore.Models;
using AppCore.Models.DTOs;
using MindTheCodeApp.Services.IServices;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using AppCore.Models.AuthModels;

namespace MindTheCodeApp.Controllers
{
    [Route("")]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService _bookService;

        public HomeController(ILogger<HomeController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
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

            var dto = _bookService.GetHomeDTO();

            return View("/Views/Home/Index.cshtml", dto);
        }
    }
}