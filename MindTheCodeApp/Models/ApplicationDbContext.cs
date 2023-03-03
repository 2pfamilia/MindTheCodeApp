using DBModelExercise.Data.Models.Auth;
using DBModelExercise.Data.Models.Books;
using DBModelExercise.Data.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace DBModelExercise.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasOne(e => e.Role)
                    .WithMany(e => e.Users)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.AddressInformation)
                    .WithMany(e => e.Users)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(e => e.Orders)
                    .WithOne(e => e.User)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasMany(e => e.Users)
                    .WithOne(e => e.Role)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<AddressInformation>(entity =>
            {
                entity.HasMany(e => e.Users)
                    .WithOne(e => e.AddressInformation)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(e => e.Orders)
                    .WithOne(e => e.AddressInformation)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(e => e.User)
                    .WithMany(e => e.Orders)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.AddressInformation)
                    .WithMany(e => e.Orders)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasOne(e => e.User)
                    .WithMany(e => e.Orders)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(e => e.BookOrder)
                    .WithOne(e => e.Order)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<BookCategory>(entity =>
            {
                entity.HasMany(e => e.Books)
                    .WithOne(e => e.Category)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<BookAuthor>(entity =>
            {
                entity.HasMany(e => e.Books)
                    .WithOne(e => e.Author)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<BookPhoto>(entity =>
            {
                entity.HasMany(e => e.Books)
                    .WithOne(e => e.Photo)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasOne(e => e.Category)
                    .WithMany(e => e.Books)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Author)
                    .WithMany(e => e.Books)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Photo)
                    .WithMany(e => e.Books)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasMany(e => e.BookOrder)
                    .WithOne(e => e.Book)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<BookOrder>(entity =>
            {
                entity.HasOne(e => e.Order)
                    .WithMany(e => e.BookOrder)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(e => e.Book)
                    .WithMany(e => e.BookOrder)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> UserEntity { get; set; }
        public DbSet<UserRole> UserRoleEntity { get; set; }
        public DbSet<AddressInformation> AddressInformationEntity { get; set; }
        public DbSet<Order> OrderEntity { get; set; }
        public DbSet<BookCategory> BookCategoryEntity { get; set; }
        public DbSet<BookAuthor> BookAuthorEntity { get; set; }
        public DbSet<BookPhoto> BookPhotoEntity { get; set; }
        public DbSet<Book> BookEntity { get; set; }
        public DbSet<BookOrder> BookOrderEntity { get; set; }

    }
}
