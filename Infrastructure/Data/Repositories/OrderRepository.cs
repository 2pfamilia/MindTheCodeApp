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

        public async Task<List<Order>> GetOrdersByUser(int userId)
        {
            var orderInfo = await _context.OrderEntity
                .Include(o => o.User)
                .Where(o => o.User.UserId.Equals(userId))
                .Include(o => o.OrderDetails)!
                .ThenInclude(od => od.Book)
                .ThenInclude(b => b.Author)
                .Include(o => o.OrderDetails)!
                .ThenInclude(od => od.Book)
                .ThenInclude(b => b.Category)
                .Where(o => o.User.UserId.Equals(userId))
                .ToListAsync();

            return orderInfo;
        }
    }
}