using Microsoft.EntityFrameworkCore;
using MindTheCodeApp.Repositories.Models.BookModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MindTheCodeApp.Repositories.Models.OrderModels
{
    [Table("Order_Details")]
    public class OrderDetails
    {
        [
            Key,
            Column("order_details_id"),
            NotNull,
            DatabaseGenerated(DatabaseGeneratedOption.Identity)
        ]
        public int? OrderDetailsId { get; set; }

        [
            Column("order_id"),
            ForeignKey("order_id"),
            NotNull
        ]
        public Order? Order { get; set; }

        [
            Column("book_id"),
            ForeignKey("book_id"),
            NotNull
        ]
        public Book? Book { get; set; }

        [
            Required,
            Column("unit_cost"),
            NotNull,
            Precision(5, 2)
        ]
        public decimal? Unitcost { get; set; }

        [
            Required,
            Column("total_cost"),
            NotNull,
            Precision(10, 2)
        ]
        public decimal? TotalCost { get; set; }

        [
            Required,
            Column("count"),
            NotNull
        ]
        public int? Count { get; set; }

    }
}
