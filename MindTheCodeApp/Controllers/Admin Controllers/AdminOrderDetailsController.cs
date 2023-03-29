using AppCore.Models.BookModels;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MindTheCodeApp.ViewModels.OrderVMs;
using Serilog;
using System.Data;

namespace MindTheCodeApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminOrderDetailsController : Controller
    {
        Serilog.ILogger myLog = Log.ForContext<AdminOrderDetailsController>();
        private readonly ApplicationDbContext _context;
        private readonly List<IndexOrderDetailsVM> IndexOrderDetailsVMs = new List<IndexOrderDetailsVM>();
        private readonly IndexOrderDetailsVM DetailsOrderDetailsVM = new IndexOrderDetailsVM();
        private readonly EditOrderDetailsVM EditDetailsVM = new EditOrderDetailsVM();

        public AdminOrderDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var orderDetails = await _context.OrderDetailsEntity.Include(o => o.Order).Include(b => b.Book)
                .ToListAsync();

            foreach (var detail in orderDetails)
            {
                var orderDetVM = new IndexOrderDetailsVM();
                var order = _context.OrderEntity.Include(u => u.User).Include(a => a.AddressInformation)
                    .FirstOrDefault(o => o.OrderId == detail.Order.OrderId);
                var orderEmail = order.User.Email;
                var bookTitle = _context.OrderDetailsEntity
                    .FirstOrDefault(o => o.OrderDetailsId == detail.OrderDetailsId).Book.Title;
                orderDetVM.IndexOrderDetailsId = (int)detail.OrderDetailsId;
                orderDetVM.OrderEmail = orderEmail;
                orderDetVM.BookTitle = bookTitle;
                orderDetVM.UnitCost = (decimal)detail.Unitcost;
                orderDetVM.TotalCost = (decimal)detail.TotalCost;
                orderDetVM.Count = (int)detail.Count;
                IndexOrderDetailsVMs.Add(orderDetVM);
            }

            return View("/Views/Admin/OrderDetails/Index.cshtml", IndexOrderDetailsVMs);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["Orders"] =
                new SelectList(_context.OrderEntity.Include(u => u.User), "OrderId",
                    "User.Email"); // Edo prepei na deixno ton user
            ViewData["Books"] = new SelectList(_context.BookEntity, "BookId", "Title");

            return View("/Views/Admin/OrderDetails/Create.cshtml");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderDetailsVM details)
        {
            try
            {
                myLog.Verbose("Start - Create");
                var order = _context.OrderEntity.FirstOrDefault(o => o.OrderId == details.OrderId);
                var book = _context.BookEntity.FirstOrDefault(b => b.BookId == details.BookId);
                var totalCost = details.UnitCost * details.Count;
                var newDetails = _context.OrderDetailsEntity.Add(new AppCore.Models.OrderModels.OrderDetails
                {
                    Order = order,
                    Book = book,
                    Unitcost = details.UnitCost,
                    TotalCost = totalCost,
                    Count = details.Count
                });

                await _context.SaveChangesAsync();
                myLog.Verbose("End - Create");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                myLog.Error(ex, "Exception on Create");
                throw;
            }
            
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderDetailsEntity == null)
            {
                return NotFound();
            }

            var detail = _context.OrderDetailsEntity.Include(o => o.Order).Include(b => b.Book)
                .FirstOrDefault(d => d.OrderDetailsId == id);
            var order = _context.OrderEntity.Include(u => u.User).Include(a => a.AddressInformation)
                .FirstOrDefault(o => o.OrderId == detail.Order.OrderId);
            var orderEmail = order.User.Email;
            var bookTitle = _context.OrderDetailsEntity.FirstOrDefault(o => o.OrderDetailsId == detail.OrderDetailsId)
                .Book.Title;

            if (detail == null)
            {
                return NotFound();
            }
            else
            {
                DetailsOrderDetailsVM.IndexOrderDetailsId = (int)detail.OrderDetailsId;
                DetailsOrderDetailsVM.OrderEmail = orderEmail;
                DetailsOrderDetailsVM.BookTitle = bookTitle;
                DetailsOrderDetailsVM.UnitCost = (decimal)detail.Unitcost;
                DetailsOrderDetailsVM.TotalCost = (decimal)detail.TotalCost;
                DetailsOrderDetailsVM.Count = (int)detail.Count;
            }

            return View("/Views/Admin/OrderDetails/Details.cshtml", DetailsOrderDetailsVM);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderDetailsEntity == null)
            {
                return NotFound();
            }

            var detail = await _context.OrderDetailsEntity
                .FirstOrDefaultAsync(m => m.OrderDetailsId == id);
            if (detail == null)
            {
                return NotFound();
            }

            return View("/Views/Admin/OrderDetails/Delete.cshtml", detail);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                myLog.Verbose("Start - Delete");
                if (_context.OrderDetailsEntity == null)
                {
                    return Problem("Entity set 'ApplicationDbContext.OrderDetailsEntity'  is null.");
                }

                var detail = await _context.OrderDetailsEntity.FindAsync(id);
                if (detail != null)
                {
                    _context.OrderDetailsEntity.Remove(detail);
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

        public IActionResult Edit(int id)
        {
            var details = _context.OrderDetailsEntity.Include(o => o.Order).Include(a => a.Book)
                .FirstOrDefault(o => o.OrderDetailsId == id);
            if (details != null)
            {
                EditDetailsVM.EditDetailsId = (int)details.OrderDetailsId;
                EditDetailsVM.OrderId = details.Order.OrderId;
                EditDetailsVM.BookId = details.Book.BookId;
                EditDetailsVM.UnitCost = (decimal)details.Unitcost;
                EditDetailsVM.TotalCost = (decimal)details.TotalCost;
                EditDetailsVM.Count = (int)details.Count;
            }

            ViewData["Orders"] =
                new SelectList(_context.OrderEntity.Include(u => u.User), "OrderId",
                    "User.Email"); // Edo prepei na deixno ton user
            ViewData["Books"] = new SelectList(_context.BookEntity, "BookId", "Title");

            return View("/Views/Admin/OrderDetails/Edit.cshtml", EditDetailsVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditOrderDetailsVM editDetailsVM)
        {
            try
            {
                myLog.Verbose("Start - Edit");
                var details = _context.OrderDetailsEntity.Include(o => o.Order).Include(a => a.Book)
                .FirstOrDefault(o => o.OrderDetailsId == id);
                if (details != null)
                {
                    if (id != editDetailsVM.EditDetailsId)
                    {
                        return NotFound();
                    }

                    details.Unitcost = editDetailsVM.UnitCost;
                    details.Count = editDetailsVM.Count;
                    details.TotalCost = editDetailsVM.UnitCost * editDetailsVM.Count;
                    details.Order.OrderId = editDetailsVM.OrderId;
                    details.Book.BookId = editDetailsVM.BookId;

                    await _context.SaveChangesAsync();
                    myLog.Verbose("End - Edit");
                }
                else
                {
                    return NotFound();
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                myLog.Error(ex, "Exception on Edit");
                throw;
            }
            
        }
    }
}