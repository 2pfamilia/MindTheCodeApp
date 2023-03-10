using MindTheCodeApp.Models.BookModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MindTheCodeApp.Models.OrderModels
{
    [Table("Order_Details")]
    public class OrderDetails
    {
        [Key, Column("order_details_id"), NotNull, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? OrderDetailsId { get; set; }

        [Column("order_id"), ForeignKey("order_id"), NotNull]
        public Order? Order { get; set; }

        [Column("book_id"), ForeignKey("book_id"), NotNull]
        public Book? Book { get; set; }

        [Required, Column("unit_cost"), NotNull]
        public double? Unitcost { get; set; }

        [Required, Column("total_cost"), NotNull]
        public double? TotalCost { get; set; }

        [Required, Column("count"), NotNull]
        public int? Count { get; set; }

    }
}
