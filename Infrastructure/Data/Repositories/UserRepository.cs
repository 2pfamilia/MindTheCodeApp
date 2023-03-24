using Microsoft.EntityFrameworkCore;
using AppCore.IRepositories;
using AppCore.Models.AuthModels;
using AppCore.Models.DTOs;
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

            _context.SaveChanges();

            return true;
        }
    }
}