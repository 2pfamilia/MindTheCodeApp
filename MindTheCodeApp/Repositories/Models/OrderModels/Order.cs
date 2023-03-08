using Microsoft.EntityFrameworkCore;
using MindTheCodeApp.Repositories.Models.AuthModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MindTheCodeApp.Repositories.Models.OrderModels
{
    [Table("Orders")]
    public class Order
    {
        [
            Key,
            Column("order_id"),
            NotNull,
            DatabaseGenerated(DatabaseGeneratedOption.Identity)
        ]
        public int OrderId { get; set; }

        [
            Required,
            Column("user_id"),
            ForeignKey("user_id"),
            NotNull
        ]
        public User? User { get; set; }

        [
            Column("is_fulfilled"),
            AllowNull
        ]
        public bool Fulfilled { get; set; } = false;

        [
            Column("is_active"),
            AllowNull
        ]
        public bool Active { get; set; } = true;

        [
            Column("is_canceled"),
            AllowNull
        ]
        public bool Canceled { get; set; } = false;

        [
            Required,
            Column("address_information_id"),
            ForeignKey("address_information_id"),
            NotNull
        ]
        public AddressInformation? AddressInformation { get; set; }

        [
            Required,
            Column("cost"),
            NotNull,
            Precision(10, 2)
        ]
        public decimal Cost { get; set; }

        [
            Column("date_created"), NotNull]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public ICollection<OrderDetails>? BookOrder { get; set; }

    }
}
