using DBModelExercise.Data;
using DBModelExercise.Data.Models.Auth;
using Microsoft.EntityFrameworkCore;

namespace MindTheCodeApp.Utils
{
    public class PopulateDb : BackgroundService
    {
        private readonly ILogger<PopulateDb> _logger;
        private readonly ApplicationDbContext _dbcontext;
        private readonly IServiceProvider _serviceProvider;

        public PopulateDb(ILogger<PopulateDb> logger, IServiceProvider serviceScopeFactory)
        {
            _logger = logger;
            _dbcontext = serviceScopeFactory.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(1000);

            _logger.LogWarning("Database population started.");

            await StartPopulation(stoppingToken);

            _logger.LogWarning("Database population finished.");
        }

        private async Task StartPopulation(CancellationToken stoppingToken)
        {
            /*
                TODO: Add population methods for models: 
                - Book
                - BookAuthor
                - BookCategory
                - BookPhoto
                - AddressInformation
                - BookOrder
                - Order
             */
            if (await _dbcontext.UserRoleEntity.AnyAsync())
                _logger.LogWarning("UserRole already populated.");
            else
            {
                _logger.LogWarning("UserRole population started.");
                await PopulateUserRole(stoppingToken);
                _logger.LogWarning("UserRole population finished.");
            }

            if (await _dbcontext.UserEntity.AnyAsync())
                _logger.LogWarning("User already populated.");
            else
            {
                _logger.LogWarning("User population started.");
                await PopulateUser(stoppingToken);
                _logger.LogWarning("User population finished.");
            }
        }
        private async Task PopulateUserRole(CancellationToken stoppingToken)
        {
            var UserRoles = new List<UserRole> {
                new()
                {
                    Code = "admin",
                    Title = "Administrator",
                    Description = "The Administrator role has the most privileges in the application",
                },
                new()
                {
                    Code = "reuser",
                    Title = "Registered User",
                    Description = "The Registered User role has basic privileges in the application",
                },
            };

            await _dbcontext.AddRangeAsync(UserRoles);
            await _dbcontext.SaveChangesAsync();
        }

        private async Task PopulateUser(CancellationToken stoppingToken)
        {
            /*
                TODO: Add addresses for normal users
             */
            var Users = new List<User>
            {
                new()
                {
                    FirstName = "Admin",
                    LastName = "Admin",
                    Email = "administrator@admin.org",
                    Birthdate = DateTime.Now,
                    Username = "admin",
                    Password = "admin",
                    Role = _dbcontext.UserRoleEntity.Single(role => role.Code.Equals("admin")),
                },
                new()
                {
                    FirstName = "Summer",
                    LastName = "Gates",
                    Email = "summer@gates.org",
                    Birthdate = DateTime.Now,
                    Username = "Summergates",
                    Password = "123456",
                    Role = _dbcontext.UserRoleEntity.Single(role => role.Code.Equals("reuser")),
                },
                new()
                {
                    FirstName = "Kael",
                    LastName = "Kelly",
                    Email = "kael@kelly.org",
                    Birthdate = DateTime.Now,
                    Username = "Kaelkelly",
                    Password = "123456",
                    Role = _dbcontext.UserRoleEntity.Single(role => role.Code.Equals("reuser")),
                },
                new()
                {
                    FirstName = "Wilson",
                    LastName = "Trevino",
                    Email = "wilson@trevino.org",
                    Birthdate = DateTime.Now,
                    Username = "Wilsontrevino",
                    Password = "123456",
                    Role = _dbcontext.UserRoleEntity.Single(role => role.Code.Equals("reuser")),
                },
            };

            await _dbcontext.AddRangeAsync(Users);
            await _dbcontext.SaveChangesAsync();
        }

    }
}
