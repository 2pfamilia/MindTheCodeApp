using Microsoft.EntityFrameworkCore;
using MindTheCodeApp.Repositories.IRepositories;
using MindTheCodeApp.Repositories.Models;
using MindTheCodeApp.Repositories.Models.OrderModels;

namespace MindTheCodeApp.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            var orders = await _context.OrderEntity.ToListAsync();
            return orders;
        }
    }
}
