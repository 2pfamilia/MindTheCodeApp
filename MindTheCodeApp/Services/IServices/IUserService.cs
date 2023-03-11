using MindTheCodeApp.Models.AuthModels;

namespace MindTheCodeApp.Services.IServices
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
    }
}
