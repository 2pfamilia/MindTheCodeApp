using DBModelExercise.Data.Models.Books;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DBModelExercise.Data.Models.Orders
{
    [Table("Books_Orders")]
    public class BookOrder
    {
        [Key, Column("book_order_id"), NotNull, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? BookOrderId { get; set; }

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
