using MindTheCodeApp.Repositories.Models.AuthModels;

namespace MindTheCodeApp.Repositories.IRepositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
    }
}
