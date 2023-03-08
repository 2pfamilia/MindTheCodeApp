using DBModelExercise.Data.Models.Orders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DBModelExercise.Data.Models.Books
{
    [Table("Books")]
    public class Book
    {
        [Key, Column("book_id"), NotNull, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [Required, Column("title"), NotNull, StringLength(100, MinimumLength = 1)]
        public string? Title { get; set; }

        [Column("description"), AllowNull, StringLength(500)]
        public string? Description { get; set; }

        [Column("category_id"), ForeignKey("category_id"), AllowNull]
        public BookCategory? Category { get; set; }

        [Column("photo_id"), ForeignKey("photo_id"), AllowNull]
        public BookPhoto? Photo { get; set; }

        [Column("count")]
        public int? Count { get; set; }

        [Column("author_id"), ForeignKey("author_id"), AllowNull]
        public BookAuthor? Author { get; set; }

        [Required, Column("price"), NotNull]
        public double? Price { get; set; }

        [Column("date_created"), NotNull]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public ICollection<OrderDetails>? BookOrder { get; set; }

    }
}
