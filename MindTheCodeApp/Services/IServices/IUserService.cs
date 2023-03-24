using AppCore.Models.AuthModels;
using AppCore.Models.DTOs;

namespace MindTheCodeApp.Services.IServices
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();

        public bool CreateUser(RegisterDTO registerDTO);
    }
}