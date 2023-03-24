using AppCore.IRepositories;
using AppCore.Models.AuthModels;
using AppCore.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using AppCore.Models.DTOs;
using MindTheCodeApp.Services.IServices;

using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppCore.Models.OrderModels;

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

        public async Task<Boolean> UpdateUserInfo(UserInfoDTO dto, int userId)
        {
            await _userRepository.UpdateUserInfo(dto, userId);

            return true;
        }

        public bool CreateUser(RegisterDTO registerDTO)
        {
            bool userCreated = false;

            //Transfer data from DTO to User
            var user = new User();

            user.Email = registerDTO.Email;

            if (!_userRepository.UserExists(user))
            {
                user.Birthdate = registerDTO.Birthday;
                user.Username = registerDTO.Username;
                user.FirstName = registerDTO.FirstName;
                user.LastName = registerDTO.LastName;
                user.Password = registerDTO.Password;

                //Create & Populate AddressInformation Object
                AddressInformation addressInformation = new AddressInformation();
                addressInformation.StreetAddress = registerDTO.StreetAddress;
                addressInformation.City = registerDTO.City;
                addressInformation.PostalCode = registerDTO.PostalCode;
                addressInformation.Country = registerDTO.Country;

                var userRole = _userRepository.CreateUserRole();
                user.Role = userRole;

                user.Role.RoleId = userRole.RoleId;

                //Save created AddressInformation in DB
                _userRepository.CreateAddress(addressInformation);

                //Give AddressInformationId to user
                user.AddressInformation = addressInformation;
                user.AddressInformation.AddressInformationId = addressInformation.AddressInformationId;



                //Send User data to DB
                _userRepository.CreateUser(user);

                userCreated = true;
                return userCreated;

            }
            else
            {
                userCreated = false;
                return userCreated;
            }
           
        }
    }
}