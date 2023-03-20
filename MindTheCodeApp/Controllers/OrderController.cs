using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppCore.Models.OrderModels;
using Infrastructure.Data;
using MindTheCodeApp.ViewModels;

namespace MindTheCodeApp.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public int BookVMId { get; set; }

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
              return _context.OrderEntity != null ? 
                          View(await _context.OrderEntity.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.OrderEntity'  is null.");
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderEntity == null)
            {
                return NotFound();
            }

            var order = await _context.OrderEntity
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            ViewData["Books"] = new SelectList(_context.BookEntity, "BookId", "Title");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderVM order)
        {
            order.BookId = BookVMId;

            var book = _context.BookEntity.FirstOrDefault(b => b.BookId == order.BookId);

            var address = _context.AddressInformationEntity.Add(new AddressInformation
            {
                StreetAddress = order.StreetAddress,
                City = order.City,
                PostalCode = order.PostalCode,
                Country = order.Country
            });

            var newOrder = _context.OrderEntity.Add(new Order
            {
                //User = customer.Entity,
                Fulfilled = order.Fulfilled,
                Active = order.Active,
                Canceled = order.Canceled,
                AddressInformation = address.Entity,
                Cost = order.Cost,
                DateCreated = order.DateCreated
            });

            var newOrderDetails = _context.OrderDetailsEntity.Add(new OrderDetails
            {
                Order = newOrder.Entity,
                Book = book,
                Unitcost = order.Unitcost,
                TotalCost = order.TotalCost,
                Count = order.Count
            });

            await _context.SaveChangesAsync();
            return View(order);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OrderEntity == null)
            {
                return NotFound();
            }

            var order = await _context.OrderEntity.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,Fulfilled,Active,Canceled,Cost,DateCreated")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OrderEntity == null)
            {
                return NotFound();
            }

            var order = await _context.OrderEntity
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OrderEntity == null)
            {
                return Problem("Entity set 'ApplicationDbContext.OrderEntity'  is null.");
            }
            var order = await _context.OrderEntity.FindAsync(id);
            if (order != null)
            {
                _context.OrderEntity.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
          return (_context.OrderEntity?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
