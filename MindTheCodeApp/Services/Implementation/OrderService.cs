using AppCore.IRepositories;
using AppCore.Models.AuthModels;
using AppCore.Models.BookModels;
using AppCore.Models.DTOs;
using AppCore.Models.OrderModels;
using MindTheCodeApp.Services.IServices;

namespace AppCore.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> CreateNewOrder(User user, Dictionary<Book, int> booksWithQuantities)
        {
            var newOrder = await _orderRepository.CreateOrder(user, booksWithQuantities);
            return newOrder;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrders();
            return orders;
        }

        public CheckoutDTO GetCheckoutDTO()
        {
            var checkoutDTO = new CheckoutDTO();
            return checkoutDTO;
        }
    }
}