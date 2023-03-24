using Microsoft.EntityFrameworkCore;
using AppCore.IRepositories;
using AppCore.Models.AuthModels;
using AppCore.Models.OrderModels;

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

        public void CreateUser(User user)
        {
            _context.UserEntity.Add(user);
            _context.SaveChanges();
        }

        public void CreateAddress(AddressInformation address)
        {
            _context.AddressInformationEntity.Add(address);
            _context.SaveChanges();
        }

        public bool UserExists(User user)
        {
            var userExists = _context.UserEntity.Any(u =>
                        u.Email.Equals(user.Email)
                    );

            return userExists;
        }

       public UserRole CreateUserRole()
        {
            var userRole = _context.UserRoleEntity.FirstOrDefault(role => role.RoleId == 2);
            return userRole;
        }

    }
}