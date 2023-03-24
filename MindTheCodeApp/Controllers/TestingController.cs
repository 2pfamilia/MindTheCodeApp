using AppCore.Models.AuthModels;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppCore.Models.DTOs;
using MindTheCodeApp.Services.IServices;

namespace MindTheCodeApp.Controllers
{
    public class TestingController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public TestingController(ApplicationDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register([FromForm] RegisterDTO registerDTO)
        {
            User user = new User();

            user.Email = registerDTO.Email;

            if(registerDTO.Email is null) 
            {
                TempData["msg"] = "<script>alert('User Name is obligatory');</script>";

                return View("/Views/Testing/Index.cshtml");

            }

            var userCreated = _userService.CreateUser(registerDTO);

            if(userCreated)
            {
                TempData["msg"] = "<script>alert('User Created');</script>";
            }
            else
            {
                TempData["msg"] = "<script>alert('User Exists');</script>";
            }

            return View("/Views/Testing/Index.cshtml");

            /*
            var userExists = _context.UserEntity.Any(u =>
                        u.Username.Equals(user.Username)
                    );
            

            if(!userExists)
            {
                TempData["msg"] = "<script>alert('User Does not Exist');</script>";
                //return NotFound();
                _userService.CreateUser(registerDTO);
                return View("/Views/Testing/Index.cshtml");
            }
            else
            {
                TempData["msg"] = "<script>alert('User Exists');</script>";
                return View("/Views/Testing/Index.cshtml");
            }
            */

            /*
            try
            {
                user = _context.UserEntity
                    .Include(u => u.Role)
                    .Single(u =>
                        u.Username.Equals(user.Username)
                    );

                TempData["msg"] = "<script>alert('User Exists');</script>";
                return View("/Views/Testing/Index.cshtml");

            }
            catch (Exception)
            {
                TempData["msg"] = "<script>alert('User Does not Exist');</script>";
                //return NotFound();
                _userService.CreateUser(registerDTO);
                return View("/Views/Testing/Index.cshtml");
            }

            //return View("/Views/Home/Index.cshtml");
            */
        }
    }
}
