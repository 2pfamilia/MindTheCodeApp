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
        private readonly IUserRepository _userRepository;

        public OrderService(IOrderRepository orderRepository, IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;   
        }

        public async Task<Order> CreateNewOrder(int userID, Dictionary<Book, int> booksWithQuantities)
        {
            var user = await _userRepository.GetUserInfo(userID);
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