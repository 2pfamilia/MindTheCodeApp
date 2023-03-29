using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MindTheCodeApp.ViewModels.BookVMs;
using Serilog;
using System.Data;

namespace MindTheCodeApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminPhotoController : Controller
    {
        Serilog.ILogger myLog = Log.ForContext<AdminPhotoController>();
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;
        private List<PhotoVM> IndexPhotosVM = new List<PhotoVM>();

        public AdminPhotoController(IWebHostEnvironment webHostEnvironment, ApplicationDbContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

        public IActionResult UploadImage()
        {
            return View("/Views/Admin/Photo/UploadImage.cshtml");
        }

        // Ena antistoixo UploadAuthor gia touw authors

        [HttpPost]
        public async Task<IActionResult> UploadImage(PhotoVM model)
        {

            if (model.IsBook)
            {
                string uniqueFileName = null;

                if (model.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "products");
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
                    FilePath = uniqueFileName,
                    IsBook = model.IsBook,
                    IsAuthor = model.IsAuthor,
                    DateCreated = DateTime.Now
                });
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
            {
                string uniqueFileName = null;

                if (model.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "authors");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using var fileStream = new FileStream(filePath, FileMode.Create);
                    await model.ImageFile.CopyToAsync(fileStream);
                }

                model.ImagePath = uniqueFileName;
                var newAuthorPhoto = _context.PhotoEntity.Add(new AppCore.Models.PhotoModels.Photo()
                {
                    Title = model.Title,
                    Description = model.Description,
                    FilePath = uniqueFileName,
                    IsBook = model.IsBook,
                    IsAuthor = model.IsAuthor,
                    DateCreated = DateTime.Now
                });
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Index()
        {
            var photos = await _context.PhotoEntity.ToListAsync();
            foreach (var photo in photos)
            {
                var photoVM = new PhotoVM();
                photoVM.PhotoId = photo.PhotoId;
                photoVM.Title = photo.Title;
                photoVM.Description = photo.Description;
                photoVM.ImagePath = photo.FilePath;
                photoVM.IsBook = photo.IsBook;
                photoVM.IsAuthor = photo.IsAuthor;
                IndexPhotosVM.Add(photoVM);
            }

            return View("/Views/Admin/Photo/Index.cshtml", IndexPhotosVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PhotoEntity == null)
            {
                return NotFound();
            }

            var order = await _context.PhotoEntity
                .FirstOrDefaultAsync(m => m.PhotoId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View("/Views/Admin/Photo/Delete.cshtml", order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                myLog.Verbose("Start - Delete");
                if (_context.PhotoEntity == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.PhotoEntity'  is null.");
                }

                var photo = await _context.PhotoEntity.FindAsync(id);
                if (photo != null)
                {
                    _context.PhotoEntity.Remove(photo);
                }

                await _context.SaveChangesAsync();
                myLog.Verbose("End - Delete");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                myLog.Error(ex, "Exception on Delete");
                throw;
            }
        }
    }
}