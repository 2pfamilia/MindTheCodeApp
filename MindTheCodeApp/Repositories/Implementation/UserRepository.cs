using Microsoft.EntityFrameworkCore;
using MindTheCodeApp.Repositories.IRepositories;
using MindTheCodeApp.Repositories.Models;
using MindTheCodeApp.Repositories.Models.AuthModels;
using System.Runtime.CompilerServices;

namespace MindTheCodeApp.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.UserEntity.ToListAsync();
            return users;
        }
    }
}
