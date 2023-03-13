using AppCore.Models.OrderModels;

namespace AppCore.Services.IServices
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrder();
    }
}
