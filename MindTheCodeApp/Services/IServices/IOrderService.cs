using AppCore.Models.OrderModels;

namespace MindTheCodeApp.Services.IServices
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrder();
    }
}