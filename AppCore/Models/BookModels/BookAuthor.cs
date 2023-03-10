using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MindTheCodeApp.Models.BookModels
{
    [Table("Books_Authors")]
    public class BookAuthor
    {
        [Key, Column("author_id"), NotNull, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AuthorId { get; set; }

        [Required, Column("name"), NotNull, StringLength(50, MinimumLength = 2)]
        public string? Name { get; set; }

        //[Required, Column("first_name"), NotNull, StringLength(50, MinimumLength = 2)]
        //public string? FirstName { get; set; }

        //[Column("last_name"), AllowNull, StringLength(50, MinimumLength = 2)]
        //public string? LastName { get; set; }

        [Column("description"), AllowNull, StringLength(500)]
        public string? Description { get; set; }

        [Column("date_created"), NotNull]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public ICollection<Book>? Books { get; set; }
    }
}
