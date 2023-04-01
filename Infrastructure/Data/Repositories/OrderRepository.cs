using Microsoft.EntityFrameworkCore;
using AppCore.IRepositories;
using AppCore.Models.OrderModels;
using AppCore.Models.BookModels;
using AppCore.Models.AuthModels;

namespace Infrastructure.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrder(User user, Dictionary<Book, int> books)
        {
            var order = new Order
            {
                User = user,
                AddressInformation = user.AddressInformation,
            };
            var orderDetails = new List<OrderDetails>();

            decimal? totalCost = 0;

            foreach (var book in books.Keys)
            {
                totalCost += book.Price;
                orderDetails.Add(new OrderDetails()
                {
                    Order = order,
                    Book = book,
                    Unitcost = book.Price,
                    TotalCost = book.Price * books[book],
                    Count = books[book],
                });
            }

            order.Cost = (int)totalCost;

            await _context.OrderEntity.AddAsync(order);
            await _context.SaveChangesAsync();
            await _context.OrderDetailsEntity.AddRangeAsync(orderDetails);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<OrderDetails> CreateOrderDetails(Order order, Book book, int count)
        {
            var orderDetails = new OrderDetails
            {
                Book = book,
                Order = order,
                Unitcost = book.Price,
                TotalCost = book.Price * count,
                Count = count
            };
            orderDetails.TotalCost = orderDetails.Count * orderDetails.Unitcost;
            await _context.OrderDetailsEntity.AddAsync(orderDetails);
            _context.SaveChanges();
            return orderDetails;
        }


        public async Task<List<Order>> GetAllOrders()
        {
            var orders = await _context.OrderEntity.ToListAsync();
            return orders;
        }

        public async Task<List<OrderDetails>> GetOrderDetailsByOrder(Order order)
        {
            var orderDetails = await _context.OrderDetailsEntity.Where(myOrderDetails => myOrderDetails.Order == order)
                .ToListAsync();
            return orderDetails;
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
                .ThenInclude(b => b.Photo)
                .Include(o => o.OrderDetails)!
                .ThenInclude(od => od.Book)
                .ThenInclude(b => b.Category)
                .Where(o => o.User.UserId.Equals(userId))
                .ToListAsync();

            return orderInfo;
        }
    }
}