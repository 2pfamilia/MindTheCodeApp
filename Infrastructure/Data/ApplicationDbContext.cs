using Microsoft.EntityFrameworkCore;
using AppCore.Models.AuthModels;
using AppCore.Models.BookModels;
using AppCore.Models.OrderModels;
using AppCore.Models.PhotoModels;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(e => e.Role)
                    .WithMany(e => e.Users)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.AddressInformation)
                    .WithMany(e => e.Users)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Orders)
                    .WithOne(e => e.User)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasMany(e => e.Users)
                    .WithOne(e => e.Role);
            });

            modelBuilder.Entity<AddressInformation>(entity =>
            {
                entity.HasMany(e => e.Users)
                    .WithOne(e => e.AddressInformation)
                    .OnDelete(DeleteBehavior.Cascade);

                // TODO: fix bug when user has changed address information.
                entity.HasMany(e => e.Orders)
                    .WithOne(e => e.AddressInformation)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(e => e.User)
                    .WithMany(e => e.Orders)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.AddressInformation)
                    .WithMany(e => e.Orders)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(e => e.OrderDetails)
                    .WithOne(e => e.Order);
            });

            modelBuilder.Entity<OrderDetails>(entity =>
            {
                entity.HasOne(e => e.Order)
                    .WithMany(e => e.OrderDetails)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Book)
                    .WithMany(e => e.OrderDetails)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.HasMany(e => e.Books)
                    .WithOne(e => e.Category);
            });

            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.HasMany(e => e.Books)
                    .WithOne(e => e.Author);

                entity.HasOne(e => e.Photo)
                    .WithMany(e => e.Authors);
            });

            modelBuilder.Entity<Photo>(entity =>
            {
                entity.HasMany(e => e.Books)
                    .WithOne(e => e.Photo);

                entity.HasMany(e => e.Authors)
                    .WithOne(e => e.Photo);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasOne(e => e.Category)
                    .WithMany(e => e.Books)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Author)
                    .WithMany(e => e.Books)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Photo)
                    .WithMany(e => e.Books)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.OrderDetails)
                    .WithOne(e => e.Book);
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> UserEntity { get; set; }
        public DbSet<UserRole> UserRoleEntity { get; set; }
        public DbSet<AddressInformation> AddressInformationEntity { get; set; }
        public DbSet<Order> OrderEntity { get; set; }
        public DbSet<OrderDetails> OrderDetailsEntity { get; set; }
        public DbSet<BookCategory> BookCategoryEntity { get; set; }
        public DbSet<BookAuthor> BookAuthorEntity { get; set; }
        public DbSet<Photo> PhotoEntity { get; set; }
        public DbSet<Book> BookEntity { get; set; }
    }
}