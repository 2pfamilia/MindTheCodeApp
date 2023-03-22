using AppCore.Models.OrderModels;

namespace AppCore.IRepositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrders();
    }
}