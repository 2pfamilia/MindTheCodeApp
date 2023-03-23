using AppCore.IRepositories;
using AppCore.Models.AuthModels;
using MindTheCodeApp.Services.IServices;

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
    }
}