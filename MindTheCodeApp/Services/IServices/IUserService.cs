using AppCore.Models.AuthModels;
using AppCore.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace MindTheCodeApp.Services.IServices
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<UserInfoDTO> GetUserInfo(int userId);
    }
}