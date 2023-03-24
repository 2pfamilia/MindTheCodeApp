using AppCore.Models.AuthModels;

namespace AppCore.IRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserInfo(int userId);
    }
}