using AppCore.Models.AuthModels;
using AppCore.Models.DTOs;

namespace AppCore.IRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserInfo(int userId);
        Task<Boolean> UpdateUserInfo(UserInfoDTO dto, int useId);
    }
}