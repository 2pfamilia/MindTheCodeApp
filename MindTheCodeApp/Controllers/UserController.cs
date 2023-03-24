using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MindTheCodeApp.Services.IServices;

namespace MindTheCodeApp.Controllers;

[Route("/Account/")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("Info")]
    [Authorize(Roles = "reuser, admin")]
    public async Task<IActionResult> InfoView()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "-1");

        var dto = await _userService.GetUserInfo(userId);

        return View("/Views/MyAccount/MyAccount.cshtml", dto);
    }

    [HttpGet("Login")]
    [AllowAnonymous]
    // Test Controller for Login.
    public IActionResult LoginView()
    {
        if (User.Identity!.IsAuthenticated)
            return RedirectToAction("Index", "Home");

        return View("/Views/Auth/Login.cshtml");
    }
}