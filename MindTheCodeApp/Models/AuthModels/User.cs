﻿using DBModelExercise.Data.Models.Orders;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DBModelExercise.Data.Models.Auth
{
    [Table("Users")]
    public class User
    {
        [Key, Column("user_id"), NotNull, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required, Column("first_name"), NotNull, StringLength(50, MinimumLength = 2)]
        public string? FirstName { get; set; }

        [Required, Column("last_name"), NotNull, StringLength(50, MinimumLength = 2)]
        public string? LastName { get; set; }

        [Required, Column("email"), NotNull, EmailAddress]
        public string? Email { get; set; }

        [Required, Column("birthdate"), NotNull]
        public DateTime? Birthdate { get; set; }

        [Required, Column("username"), NotNull, StringLength(36, MinimumLength = 5)]
        public string? Username { get; set; }

        [Required, Column("password"), NotNull, StringLength(24, MinimumLength = 6), PasswordPropertyText]
        public string? Password { get; set; }

        [Column("address_information_id"), ForeignKey("address_information_id"), AllowNull]
        public AddressInformation? AddressInformation { get; set; }

        [Column("role_id"), ForeignKey("role_id"), AllowNull]
        public UserRole? Role { get; set; }

        [Column("date_created"), NotNull]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public ICollection<Order>? Orders { get; set; }
    }
}
