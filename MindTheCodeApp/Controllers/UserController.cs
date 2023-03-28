using System.Security.Claims;
using AppCore.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindTheCodeApp.Services.IServices;
using AppCore.Models.AuthModels;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace MindTheCodeApp.Controllers;

[Route("/my-acccount/")]
public class UserController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IUserService _userService;

    public UserController(IUserService userService, ApplicationDbContext context)
    {
        _userService = userService;
        _context = context;
    }

    [HttpGet("")]
    [Authorize(Roles = "reuser, admin")]
    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "-1");

        var dto = await _userService.GetUserInfo(userId);

        return View(dto);
    }

    [HttpPost("change-info")]
    [Authorize(Roles = "reuser, admin")]
    public async Task<IActionResult> InfoUpdate([FromForm] UserInfoDTO dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "-1");

        await _userService.UpdateUserInfo(dto, userId);

        return RedirectToAction("Index", "User");
    }

    [HttpPost("change-password")]
    [Authorize(Roles = "reuser, admin")]
    public async Task<IActionResult> UpdateUserPassword([FromForm] UserChangePasswordDTO dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "-1");

        await _userService.UpdateUserPassword(dto, userId);

        return RedirectToAction("Index", "User");
    }

   
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromForm] User user)
    {
        if (User.Identity!.IsAuthenticated)
            return RedirectToAction("Index", "Home");

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
            return RedirectToAction("Index", "Home");
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

    [HttpGet("logout")]
    [Authorize(Roles = "reuser, admin")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("register")]
    [AllowAnonymous]
    public IActionResult Register()
    {
        if (User.Identity!.IsAuthenticated)
            return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromForm] RegisterDTO registerDTO)
    {
        if (User.Identity!.IsAuthenticated)
            return RedirectToAction("Index", "Home");

        User user = new User();

        //user.Email = registerDTO.Email;

        if (registerDTO.Email is null || registerDTO.FirstName is null || registerDTO.LastName is null
            || registerDTO.City is null || registerDTO.StreetAddress is null || registerDTO.Country is null
            || registerDTO.PostalCode is null || registerDTO.Phone is null || registerDTO.Password is null)
        {
            TempData["msg"] = "<script>alert('All Fields are Obligatory');</script>";

            return View();
        }

        var userCreated = await _userService.CreateUser(registerDTO);

        if (userCreated)
        {
            TempData["msg"] = "<script>alert('User Created');</script>";
        }
        else
        {
            TempData["msg"] = "<script>alert('User Already Exists');</script>";
        }

        return View();
    }

    [HttpGet("cart")]
    [AllowAnonymous]
    public IActionResult Cart()
    {
       return View();
    }
}