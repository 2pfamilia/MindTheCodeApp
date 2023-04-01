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
using Microsoft.AspNetCore.Localization;

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

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
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

        [HttpGet("AboutUs")]
        public async Task<IActionResult> AboutUs()
        {
            return View("/Views/Home/AboutUs.cshtml");
        }

        [HttpGet("ContactUs")]
        public async Task<IActionResult> ContactUs()
        {
            return View("/Views/Home/ContactUs.cshtml");
        }

        [HttpGet("Shipping")]
        public async Task<IActionResult> Shipping()
        {
            return View("/Views/Home/Shipping.cshtml");
        }

        [HttpGet("PaymentMethods")]
        public async Task<IActionResult> PaymentMethods()
        {
            return View("/Views/Home/PaymentMethods.cshtml");
        }

        [HttpGet("TermsOfUse")]
        public async Task<IActionResult> TermsOfUse()
        {
            return View("/Views/Home/TermsOfUse.cshtml");
        }
    }
}