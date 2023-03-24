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
                Active = true,
                User = user,
                Fulfilled = false,
                Canceled = false,
                AddressInformation = user.AddressInformation,
                DateCreated = DateTime.Now
            };
            var orderDetails = new List<OrderDetails>();
            foreach (var book in books.Keys)
            {
                orderDetails.Add(await CreateOrderDetails(order, book, books[book]));
            }
            await _context.OrderEntity.AddAsync(order);
            _context.SaveChanges();
            return order;
        }

        public async Task<OrderDetails> CreateOrderDetails(Order order, Book book, int count)
        {
            var orderDetails = new OrderDetails
            {
                Book = book,
                Order = order,
                Unitcost = book.Price,
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
            var orderDetails = await _context.OrderDetailsEntity.Where(myOrderDetails=>myOrderDetails.Order== order).ToListAsync();
            return orderDetails;
        }
    }
}