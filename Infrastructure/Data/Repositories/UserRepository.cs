using Microsoft.EntityFrameworkCore;
using AppCore.IRepositories;
using AppCore.Models.AuthModels;

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

        public async Task<User> GetUserInfo(int userId)
        {
            var user = await _context.UserEntity
                .Include(u => u.AddressInformation)
                .SingleAsync(u => u.UserId.Equals(userId));

            return user;
        }
    }
}