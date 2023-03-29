using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppCore.Models.OrderModels;
using Infrastructure.Data;
using MindTheCodeApp.ViewModels.OrderVMs;
using Serilog;
using System.Net;
using AppCore.Models.DTOs;
using MindTheCodeApp.Services.IServices;

namespace MindTheCodeApp.Controllers
{
    public class AdminOrderController : Controller
    {
        Serilog.ILogger myLog = Log.ForContext<AdminOrderController>();
        private readonly ApplicationDbContext _context;
        private readonly List<IndexOrderVM> IndexOrderVMs = new List<IndexOrderVM>();
        private readonly IndexOrderVM DetailsOrderVM = new IndexOrderVM();
        private readonly EditOrderVM EditOrderVM = new EditOrderVM();
        private readonly OrderEmailDTO OrderEmailDTO = new OrderEmailDTO();
        private IEmailService _emailService;

        public AdminOrderController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            var orders = await _context.OrderEntity.Include(c => c.User).Include(a => a.AddressInformation)
                .ToListAsync();

            foreach (var order in orders)
            {
                var orderVM = new IndexOrderVM();
                var email = _context.OrderEntity.FirstOrDefault(u => u.OrderId == order.OrderId).User.Email;
                var address = _context.OrderEntity.FirstOrDefault(a => a.OrderId == order.OrderId).AddressInformation
                    .StreetAddress;
                orderVM.IndexOrderId = order.OrderId;
                if (email != null)
                {
                    orderVM.CustomerEmail = email;
                }

                if (address != null)
                {
                    orderVM.Address = address;
                }

                orderVM.Fulfilled = order.Fulfilled;
                orderVM.Active = order.Active;
                orderVM.Cancelled = order.Canceled;
                orderVM.Cost = order.Cost;
                orderVM.OrderCreated = order.DateCreated;
                IndexOrderVMs.Add(orderVM);
            }

            return View("/Views/Admin/Order/Index.cshtml", IndexOrderVMs);
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OrderEntity == null)
            {
                return NotFound();
            }

            var order = await _context.OrderEntity.Include(u => u.User).Include(a => a.AddressInformation)
                .FirstOrDefaultAsync(o => o.OrderId == id);
            var user = _context.OrderEntity.FirstOrDefault(u => u.OrderId == order.OrderId).User.Email;
            var address = _context.OrderEntity.FirstOrDefault(a => a.OrderId == order.OrderId).AddressInformation
                .StreetAddress;


            if (order == null)
            {
                return NotFound();
            }
            else
            {
                DetailsOrderVM.IndexOrderId = order.OrderId;
                DetailsOrderVM.Address = address;
                DetailsOrderVM.CustomerEmail = user;
                DetailsOrderVM.Fulfilled = order.Fulfilled;
                DetailsOrderVM.Active = order.Active;
                DetailsOrderVM.Cancelled = order.Canceled;
                DetailsOrderVM.Cost = order.Cost;
                DetailsOrderVM.OrderCreated = order.DateCreated;
            }

            return View("/Views/Admin/Order/Details.cshtml", DetailsOrderVM);
        }

        // GET: Order/Create
        public IActionResult Create()
        {
            ViewData["Users"] = new SelectList(_context.UserEntity, "UserId", "Email");
            ViewData["Addresses"] =
                new SelectList(_context.AddressInformationEntity, "AddressInformationId", "StreetAddress");
            return View("/Views/Admin/Order/Create.cshtml");
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOrderVM order)
        {
            try
            {
                myLog.Verbose("Start - Create");
                var user = _context.UserEntity.FirstOrDefault(u => u.UserId == order.UserId);
                var address =
                    _context.AddressInformationEntity.FirstOrDefault(a =>
                        a.AddressInformationId == order.AddressInformationId);

                var newOrder = _context.OrderEntity.Add(new Order
                {
                    User = user,
                    Fulfilled = order.Fulfilled,
                    Active = order.Active,
                    Canceled = order.Canceled,
                    AddressInformation = address,
                    Cost = order.Cost,
                    DateCreated = DateTime.Now
                });

                OrderEmailDTO.FirstName = user.FirstName;
                OrderEmailDTO.LastName = user.LastName;
                OrderEmailDTO.CustomerEmail = user.Email;
                OrderEmailDTO.TotalCost = newOrder.Entity.Cost;
                OrderEmailDTO.StreetAddress = address.StreetAddress;
                await _emailService.SendOrderConfirmationEmail(OrderEmailDTO);
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

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var order = _context.OrderEntity.Include(u => u.User).Include(a => a.AddressInformation)
                .FirstOrDefault(o => o.OrderId == id);
            if (order != null)
            {
                EditOrderVM.EditOrderId = order.OrderId;
                EditOrderVM.Fulfilled = order.Fulfilled;
                EditOrderVM.Active = order.Active;
                EditOrderVM.Canceled = order.Canceled;
                EditOrderVM.UserId = order.User.UserId;
                EditOrderVM.AddressInformationId = order.AddressInformation.AddressInformationId;
                EditOrderVM.Cost = order.Cost;
                EditOrderVM.OrderCreated = order.DateCreated;
            }

            ViewData["Users"] = new SelectList(_context.UserEntity, "UserId", "Email");
            ViewData["Addresses"] =
                new SelectList(_context.AddressInformationEntity, "AddressInformationId", "StreetAddress");

            return View("/Views/Admin/Order/Edit.cshtml", EditOrderVM);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditOrderVM editOrderVM)
        {
            try
            {
                myLog.Verbose("Start - Edit");
                var order = _context.OrderEntity.Include(u => u.User).Include(a => a.AddressInformation)
                .FirstOrDefault(o => o.OrderId == id);
                if (order != null)
                {
                    if (id != order.OrderId)
                    {
                        return NotFound();
                    }

                    order.Fulfilled = editOrderVM.Fulfilled;
                    order.Active = editOrderVM.Active;
                    order.Canceled = editOrderVM.Canceled;
                    order.User.UserId = editOrderVM.UserId;
                    order.AddressInformation.AddressInformationId = editOrderVM.AddressInformationId;
                    order.Cost = editOrderVM.Cost;
                    order.DateCreated = editOrderVM.OrderCreated;

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

            return View("/Views/Admin/Order/Delete.cshtml", order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                myLog.Verbose("Start - Delete");
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
                myLog.Verbose("End - Delete");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                myLog.Error(ex, "Exception on Delete");
                throw;
            }
            
        }

        private bool OrderExists(int id)
        {
            return (_context.OrderEntity?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}