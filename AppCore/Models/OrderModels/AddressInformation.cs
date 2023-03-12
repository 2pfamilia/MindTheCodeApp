using AppCore.Models.AuthModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AppCore.Models.OrderModels
{
    [Table("Address_Information")]
    public class AddressInformation
    {
        [
            Key, 
            Column("address_information_id"), 
            NotNull, 
            DatabaseGenerated(DatabaseGeneratedOption.Identity)
        ]
        public int AddressInformationId { get; set; }

        [
            Required,
            Column("street_address"),
            NotNull,
            StringLength(100)
        ]
        public string? StreetAddress { get; set; }

        [
            Required, Column("city"),
            NotNull,
            StringLength(100)
        ]
        public string? City { get; set; }

        [
            Required, 
            Column("postal_code"), 
            NotNull, 
            StringLength(15)
        ]
        public string? PostalCode { get; set; }

        [
            Required, 
            Column("country"),
            NotNull, 
            StringLength(100)
        ]
        public string? Country { get; set; }

        public ICollection<User>? Users { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}