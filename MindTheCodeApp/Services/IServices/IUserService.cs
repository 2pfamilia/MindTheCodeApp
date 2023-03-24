﻿using AppCore.Models.AuthModels;
using AppCore.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using AppCore.Models.DTOs;

namespace MindTheCodeApp.Services.IServices
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsers();
        Task<UserInfoDTO> GetUserInfo(int userId);
        Task<Boolean> UpdateUserInfo(UserInfoDTO dto, int userId);

        public bool CreateUser(RegisterDTO registerDTO);
    }
}