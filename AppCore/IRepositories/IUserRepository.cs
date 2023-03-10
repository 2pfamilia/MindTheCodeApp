using MindTheCodeApp.Models.AuthModels;

namespace MindTheCodeApp.IRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
    }
}
