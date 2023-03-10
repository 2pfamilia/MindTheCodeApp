using MindTheCodeApp.Models.OrderModels;

namespace MindTheCodeApp.IRepositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrders();
    }
}
