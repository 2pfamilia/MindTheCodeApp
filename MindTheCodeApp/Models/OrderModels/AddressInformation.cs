using DBModelExercise.Data.Models.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DBModelExercise.Data.Models.Orders
{
    [Table("Address_Information")]
    public class AddressInformation
    {
        [Key, Column("address_information_id"), NotNull, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressInformationId { get; set; }

        [Required, Column("street_address"), NotNull]
        public string? StreetAddress { get; set; }

        [Required, Column("city"), NotNull]
        public string? City { get; set; }

        [Required, Column("postal_code"), NotNull]
        public string? PostalCode { get; set; }

        [Required, Column("country"), NotNull]
        public string? Country { get; set; }

        public ICollection<User>? Users { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}