using AppCore.Models.AuthModels;
using AppCore.Models.BookModels;
using AppCore.Models.OrderModels;

namespace MindTheCodeApp.Services.IServices
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> CreateNewOrder(User user, Dictionary<Book, int> booksWithQuantities);
    }
}