using Microsoft.EntityFrameworkCore;
using AppCore.IRepositories;
using AppCore.Models.AuthModels;
using AppCore.Models.DTOs;
using AppCore.Models.OrderModels;
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

        public async Task<bool> UserExists(User user)
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


        public async Task<User> GetUserInfo(int userId)
        {
            var user = await _context.UserEntity
                .Include(u => u.AddressInformation)
                .SingleAsync(u => u.UserId.Equals(userId));

            return user;
        }

        public async Task<bool> UpdateUserInfo(UserInfoDTO dto, int userId)
        {
            var user = _context.UserEntity
                .Include(u => u.AddressInformation)
                .Single(u => u.UserId.Equals(userId));

            user.Email = dto.Email;
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Phone = dto.Phone;

            if (user.AddressInformation is null)
            {
                var addressInformation = new AddressInformation()
                {
                    Country = dto.Country,
                    City = dto.City,
                    PostalCode = dto.ZipCode,
                    StreetAddress = dto.StreetAddress,
                };
                user.AddressInformation = addressInformation;
            }
            else
            {
                user.AddressInformation.Country = dto.Country;
                user.AddressInformation.City = dto.City;
                user.AddressInformation.PostalCode = dto.ZipCode;
                user.AddressInformation.StreetAddress = dto.StreetAddress;
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateUserPassword(UserChangePasswordDTO dto, int userId)
        {
            var user = _context.UserEntity.Single(u => u.UserId.Equals(userId) && u.Password.Equals(dto.OldPassword));

            user.Password = dto.NewPassword;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}