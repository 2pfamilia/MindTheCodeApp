using MindTheCodeApp.Repositories.IRepositories;
using MindTheCodeApp.Repositories.Models.AuthModels;
using MindTheCodeApp.Services.IServices;

namespace MindTheCodeApp.Services.Implementation
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
