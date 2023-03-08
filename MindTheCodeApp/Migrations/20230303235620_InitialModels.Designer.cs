﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MindTheCodeApp.Repositories.Models;

#nullable disable

namespace MindTheCodeApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230303235620_InitialModels")]
    partial class InitialModels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DBModelExercise.Data.Models.Auth.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime?>("Birthdate")
                        .IsRequired()
                        .HasColumnType("datetime2")
                        .HasColumnName("birthdate");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2")
                        .HasColumnName("date_created");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(24)
                        .HasColumnType("nvarchar(24)")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnName("username");

                    b.Property<int?>("address_information_id")
                        .HasColumnType("int");

                    b.Property<int?>("role_id")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("address_information_id");

                    b.HasIndex("role_id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DBModelExercise.Data.Models.Auth.UserRole", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)")
                        .HasColumnName("code");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2")
                        .HasColumnName("date_created");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("title");

                    b.HasKey("RoleId");

                    b.ToTable("User_Roles");
                });

            modelBuilder.Entity("DBModelExercise.Data.Models.Books.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("book_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"));

                    b.Property<int?>("Count")
                        .HasColumnType("int")
                        .HasColumnName("count");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2")
                        .HasColumnName("date_created");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("description");

                    b.Property<double?>("Price")
                        .IsRequired()
                        .HasColumnType("float")
                        .HasColumnName("price");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("title");

                    b.Property<int?>("author_id")
                        .HasColumnType("int");

                    b.Property<int?>("category_id")
                        .HasColumnType("int");

                    b.Property<int?>("photo_id")
                        .HasColumnType("int");

                    b.HasKey("BookId");

                    b.HasIndex("author_id");

                    b.HasIndex("category_id");

                    b.HasIndex("photo_id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("DBModelExercise.Data.Models.Books.BookAuthor", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("author_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuthorId"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2")
                        .HasColumnName("date_created");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("name");

                    b.HasKey("AuthorId");

                    b.ToTable("Books_Authors");
                });

            modelBuilder.Entity("DBModelExercise.Data.Models.Books.BookCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("category_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)")
                        .HasColumnName("code");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2")
                        .HasColumnName("date_created");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("description");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("title");

                    b.HasKey("CategoryId");

                    b.ToTable("Books_Category");
                });

            modelBuilder.Entity("DBModelExercise.Data.Models.Books.BookPhoto", b =>
                {
                    b.Property<int>("PhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("photo_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PhotoId"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2")
                        .HasColumnName("date_created");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("description");

                    b.Property<byte[]>("File")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("file");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("title");

                    b.HasKey("PhotoId");

                    b.ToTable("Books_Photos");
                });

            modelBuilder.Entity("DBModelExercise.Data.Models.Orders.AddressInformation", b =>
                {
                    b.Property<int>("AddressInformationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("address_information_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressInformationId"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("city");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("country");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("postal_code");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("street_address");

                    b.HasKey("AddressInformationId");

                    b.ToTable("Address_Information");
                });

            modelBuilder.Entity("DBModelExercise.Data.Models.Orders.BookOrder", b =>
                {
                    b.Property<int?>("BookOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("book_order_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int?>("BookOrderId"));

                    b.Property<int?>("Count")
                        .IsRequired()
                        .HasColumnType("int")
                        .HasColumnName("count");

                    b.Property<double?>("TotalCost")
                        .IsRequired()
                        .HasColumnType("float")
                        .HasColumnName("total_cost");

                    b.Property<double?>("Unitcost")
                        .IsRequired()
                        .HasColumnType("float")
                        .HasColumnName("unit_cost");

                    b.Property<int>("book_id")
                        .HasColumnType("int");

                    b.Property<int>("order_id")
                        .HasColumnType("int");

                    b.HasKey("BookOrderId");

                    b.HasIndex("book_id");

                    b.HasIndex("order_id");

                    b.ToTable("Books_Orders");
                });

            modelBuilder.Entity("DBModelExercise.Data.Models.Orders.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("order_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<bool>("Active")
                        .HasColumnType("bit")
                        .HasColumnName("is_active");

                    b.Property<bool>("Canceled")
                        .HasColumnType("bit")
                        .HasColumnName("is_canceled");

                    b.Property<double>("Cost")
                        .HasColumnType("float")
                        .HasColumnName("cost");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2")
                        .HasColumnName("date_created");

                    b.Property<bool>("Fulfilled")
                        .HasColumnType("bit")
                        .HasColumnName("is_fulfilled");

                    b.Property<int>("address_information_id")
                        .HasColumnType("int");

                    b.Property<int>("user_id")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("address_information_id");

                    b.HasIndex("user_id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DBModelExercise.Data.Models.Auth.User", b =>
                {
                    b.HasOne("DBModelExercise.Data.Models.Orders.AddressInformation", "AddressInformation")
                        .WithMany("Users")
                        .HasForeignKey("address_information_id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("DBModelExercise.Data.Models.Auth.UserRole", "Role")
                        .WithMany("Users")
                        .HasForeignKey("role_id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("AddressInformation");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DBModelExercise.Data.Models.Books.Book", b =>
                {
                    b.HasOne("DBModelExercise.Data.Models.Books.BookAuthor", "Author")
                        .WithMany("Books")
                        .HasForeignKey("author_id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("DBModelExercise.Data.Models.Books.BookCategory", "Category")
                        .WithMany("Books")
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("DBModelExercise.Data.Models.Books.BookPhoto", "Photo")
                        .WithMany("Books")
                        .HasForeignKey("photo_id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Author");

                    b.Navigation("Category");

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("DBModelExercise.Data.Models.Orders.BookOrder", b =>
                {
                    b.HasOne("DBModelExercise.Data.Models.Books.Book", "Book")
                        .WithMany("BookOrder")
                        .HasForeignKey("book_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DBModelExercise.Data.Models.Orders.Order", "Order")
                        .WithMany("BookOrder")
                        .HasForeignKey("order_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("DBModelExercise.Data.Models.Orders.Order", b =>
                {
                    b.HasOne("DBModelExercise.Data.Models.Orders.AddressInformation", "AddressInformation")
                        .WithMany("Orders")
                        .HasForeignKey("address_information_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("DBModelExercise.Data.Models.Auth.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("AddressInformation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DBModelExercise.Data.Models.Auth.User", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("DBModelExercise.Data.Models.Auth.UserRole", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("DBModelExercise.Data.Models.Books.Book", b =>
                {
                    b.Navigation("BookOrder");
                });

            modelBuilder.Entity("DBModelExercise.Data.Models.Books.BookAuthor", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("DBModelExercise.Data.Models.Books.BookCategory", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("DBModelExercise.Data.Models.Books.BookPhoto", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("DBModelExercise.Data.Models.Orders.AddressInformation", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("DBModelExercise.Data.Models.Orders.Order", b =>
                {
                    b.Navigation("BookOrder");
                });
#pragma warning restore 612, 618
        }
    }
}
