using Microsoft.EntityFrameworkCore;
using MindTheCodeApp.IRepositories;
using MindTheCodeApp.Models.AuthModels;
using MindTheCodeApp.Repositories.Models;
using System.Runtime.CompilerServices;

namespace MindTheCodeApp.Repositories
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
