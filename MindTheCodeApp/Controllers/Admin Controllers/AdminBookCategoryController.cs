﻿using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MindTheCodeApp.ViewModels.BookVMs;
using Serilog;
using System.Data;

namespace MindTheCodeApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminBookCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        Serilog.ILogger myLog = Log.ForContext<AdminBookCategoryController>();
        private List<BookCategoryVM> IndexCategoriesVM { get; set; } = new();
        private BookCategoryVM DetailsBookCategoryVM { get; set; } = new BookCategoryVM();
        private EditBookCategoryVM EditBookCategoryVM { get; set; } = new EditBookCategoryVM();

        public AdminBookCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.BookCategoryEntity.ToListAsync();

            foreach (var cat in categories)
            {
                var categoryVM = new BookCategoryVM();
                categoryVM.CategoryId = cat.CategoryId;
                categoryVM.Code = cat.Code;
                categoryVM.Title = cat.Title;
                categoryVM.Description = cat.Description;
                IndexCategoriesVM.Add(categoryVM);
            }

            return View("/Views/Admin/BookCategory/Index.cshtml", IndexCategoriesVM);
        }

        public IActionResult Create()
        {
            return View("/Views/Admin/BookCategory/Create.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> Create(BookCategoryVM categoryVM)
        {
            try
            {
                myLog.Verbose("Start - Create");
                var newCategory = _context.BookCategoryEntity.Add(new AppCore.Models.BookModels.BookCategory
                {
                    Code = categoryVM.Code,
                    Title = categoryVM.Title,
                    Description = categoryVM.Description,
                    DateCreated = DateTime.Now
                });

                await _context.SaveChangesAsync();
                myLog.Verbose("End - Create");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                myLog.Error(ex, "Exception at Create");
                return null;
            }
            
        }

        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                myLog.Verbose("Start - Delete");
                if (id == null || _context.BookCategoryEntity == null)
                {
                    return NotFound();
                }

                var category = await _context.BookCategoryEntity
                    .FirstOrDefaultAsync(m => m.CategoryId == id);
                if (category == null)
                {
                    return NotFound();
                }
                myLog.Verbose("End - Delete");
                return View("/Views/Admin/BookCategory/Delete.cshtml", category);
            }
            catch (Exception ex)
            {
                myLog.Error(ex, "Exception at Delete");
                throw;
            }
            
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.BookCategoryEntity == null)
            {
                return Problem("Entity set 'ApplicationDbContext.BookCategoryEntity'  is null.");
            }

            var category = await _context.BookCategoryEntity.FindAsync(id);
            if (category != null)
            {
                _context.BookCategoryEntity.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.BookCategoryEntity == null)
            {
                return NotFound();
            }

            var category = await _context.BookCategoryEntity
                .FirstOrDefaultAsync(m => m.CategoryId == id);

            if (category == null)
            {
                return NotFound();
            }
            else
            {
                DetailsBookCategoryVM.CategoryId = category.CategoryId;
                DetailsBookCategoryVM.Code = category.Code;
                DetailsBookCategoryVM.Title = category.Title;
                DetailsBookCategoryVM.Description = category.Description;
            }

            return View("/Views/Admin/BookCategory/Details.cshtml", DetailsBookCategoryVM);
        }

        public IActionResult Edit(int id)
        {
            var category = _context.BookCategoryEntity.FirstOrDefault(b => b.CategoryId == id);
            if (category != null)
            {
                EditBookCategoryVM.EditBookCategoryId = category.CategoryId;
                EditBookCategoryVM.Code = category.Code;
                EditBookCategoryVM.Title = category.Title;
                EditBookCategoryVM.Description = category.Description;
            }

            return View("/Views/Admin/BookCategory/Edit.cshtml", EditBookCategoryVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditBookCategoryVM editBookCategoryVM)
        {
            try
            {
                myLog.Verbose("Start - Edit");
                var category = _context.BookCategoryEntity.FirstOrDefault(b => b.CategoryId == id);
                if (category != null)
                {
                    if (id != category.CategoryId)
                    {
                        return NotFound();
                    }

                    category.Code = editBookCategoryVM.Code;
                    category.Title = editBookCategoryVM.Title;
                    category.Description = editBookCategoryVM.Description;
                    myLog.Verbose("End - Edit");
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return NotFound();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                myLog.Error(ex, "Exception at Edit");
                throw;
            }
            
        }
    }
}