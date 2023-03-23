using AppCore.IRepositories;
using AppCore.Models.AuthModels;
using AppCore.Models.DTOs;
using MindTheCodeApp.Services.IServices;

namespace AppCore.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;

        public UserService(IUserRepository userRepository, IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return users;
        }

        public async Task<UserInfoDTO> GetUserInfo(int userId)
        {
            var userInfo = await _userRepository.GetUserInfo(userId);
            var orderInfo = await _orderRepository.GetOrdersByUser(userId);

            var dto = new UserInfoDTO
            {
                Email = userInfo.Email,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                Country = userInfo.AddressInformation?.Country ?? "",
                City = userInfo.AddressInformation?.City ?? "",
                ZipCode = userInfo.AddressInformation?.PostalCode ?? "",
                StreetAddress = userInfo.AddressInformation?.StreetAddress ?? "",
                Phone = userInfo.Phone,
                Orders = orderInfo
            };

            return dto;
        }
    }
}