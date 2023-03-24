using AppCore.Models.AuthModels;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AppCore.Models.DTOs;
using MindTheCodeApp.Services.IServices;

namespace MindTheCodeApp.Controllers.Auth
{
    [Route("/Auth/")]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public AuthController(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] User user)
        {
            if (user is null || user.Username is null || user.Password is null)
                return BadRequest();

            // Check if user exists in database with the correct password.
            try
            {
                user = _context.UserEntity
                    .Include(u => u.Role)
                    .Single(u =>
                        u.Username.Equals(user.Username) &&
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
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role!.Code),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

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
        public async Task<IActionResult> RegisterView()
        {
            return View("/Views/Auth/Register.cshtml");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO registerDTO)
        {
            User user = new User();

            //user.Email = registerDTO.Email;

            if (registerDTO.Email is null || registerDTO.FirstName is null || registerDTO.LastName is null 
                || registerDTO.City is null || registerDTO.StreetAddress is null || registerDTO.Country is null)
            {
                TempData["msg"] = "<script>alert('All Fields are Obligatory');</script>";

                return View("/Views/Auth/Register.cshtml");

            }

            var userCreated = _userService.CreateUser(registerDTO);

            if (userCreated)
            {
                TempData["msg"] = "<script>alert('User Created');</script>";
            }
            else
            {
                TempData["msg"] = "<script>alert('User Already Exists');</script>";
            }

            return View("/Views/Auth/Register.cshtml");
        }

    }
}