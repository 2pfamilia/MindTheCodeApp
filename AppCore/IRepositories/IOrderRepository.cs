using MindTheCodeApp.Repositories.Models.OrderModels;

namespace MindTheCodeApp.Repositories.IRepositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrders();
    }
}
