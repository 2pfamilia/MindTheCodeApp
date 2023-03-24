using AppCore.IRepositories;
using AppCore.Models.AuthModels;
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

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return users;
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