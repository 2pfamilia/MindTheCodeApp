using Microsoft.EntityFrameworkCore;
using AppCore.IRepositories;
using AppCore.Models.OrderModels;

namespace Infrastructure.Data.Repositories
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
