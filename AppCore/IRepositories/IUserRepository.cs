using AppCore.Models.AuthModels;
using AppCore.Models.DTOs;
using AppCore.Models.OrderModels;

namespace AppCore.IRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();

        public void CreateUser(User user);
        public bool UserExists(User user);

        public void CreateAddress(AddressInformation address);

        public UserRole CreateUserRole();
        Task<User> GetUserInfo(int userId);
        Task<Boolean> UpdateUserInfo(UserInfoDTO dto, int useId);
    }
}