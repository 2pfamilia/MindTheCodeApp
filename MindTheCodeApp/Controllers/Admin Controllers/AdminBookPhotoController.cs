using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MindTheCodeApp.ViewModels.BookVMs;
using Serilog;

namespace MindTheCodeApp.Controllers
{
    public class AdminBookPhotoController : Controller
    {
        Serilog.ILogger myLog = Log.ForContext<AdminBookPhotoController>();
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;
        private List<BookPhotoVM> IndexPhotosVM = new List<BookPhotoVM>();

        public AdminBookPhotoController(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public IActionResult UploadImage()
        {
            return View("/Views/Admin/BookPhoto/UploadImage.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(BookPhotoVM model)
        {
            string uniqueFileName = null;

            if (model.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "UploadedImages");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await model.ImageFile.CopyToAsync(fileStream);
            }

            model.ImagePath = uniqueFileName;
            var newBookPhoto = _context.PhotoEntity.Add(new AppCore.Models.PhotoModels.Photo()
            {
                Title = model.Title,
                Description = model.Description,
                FilePath = uniqueFileName
            });
            await _context.SaveChangesAsync();

            return RedirectToPage("/Views/Admin/Book/Index.cshtml");
        }

        public async Task<IActionResult> Index()
        {
            var photos = await _context.PhotoEntity.ToListAsync();
            foreach (var photo in photos)
            {
                var photoVM = new BookPhotoVM();
                photoVM.PhotoId = photo.PhotoId;
                photoVM.Title = photo.Title;
                photoVM.Description = photo.Description;
                photoVM.ImagePath = photo.FilePath;
                IndexPhotosVM.Add(photoVM);
            }

            return View("/Views/Admin/BookPhoto/Index.cshtml", IndexPhotosVM);
        }
    }
}