using AppCore.Models.AuthModels;

namespace AppCore.Services.IServices
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
    }
}
