using AppCore.Models.AuthModels;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MindTheCodeApp.Controllers.Auth
{
    [Route("/Auth/")]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] User user)
        {
            if (user is null || user.Email is null || user.Password is null)
                return BadRequest();

            // Check if user exists in database with the correct password.
            try
            {
                user = _context.UserEntity
                    .Include(u => u.Role)
                    .Single(u =>
                        u.Email.Equals(user.Email) &&
                        u.Password.Equals(user.Password)
                    );
            }
            catch (Exception)
            {
                return NotFound();
            }

            // Create claims for the user
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role.Code),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Logout")]
        [Authorize(Roles = "reuser, admin")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("Register")]
        public async Task<IActionResult> Register()
        {
            return View("/Views/MyAccount/MyAccount.cshtml");
        }
    }
}