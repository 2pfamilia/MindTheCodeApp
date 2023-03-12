using Microsoft.EntityFrameworkCore;
using AppCore.IRepositories;
using AppCore.Models.AuthModels;
using System.Runtime.CompilerServices;

namespace Infrastructure.Data.Repositories
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
