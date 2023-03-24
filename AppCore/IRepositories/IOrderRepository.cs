using AppCore.Models.OrderModels;
using AppCore.Models.BookModels;
using AppCore.Models.AuthModels;

namespace AppCore.IRepositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> CreateOrder(User user, Dictionary<Book,int> books);
        Task<OrderDetails> CreateOrderDetails(Order oreder, Book book, int count);
        Task<List<OrderDetails>> GetOrderDetailsByOrder(Order order);
    }
}