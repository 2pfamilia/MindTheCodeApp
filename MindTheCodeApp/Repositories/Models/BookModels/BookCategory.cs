using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MindTheCodeApp.Repositories.Models.BookModels
{
    [Table("Books_Category")]
    public class BookCategory
    {
        [
            Key,
            Column("category_id"),
            NotNull,
            DatabaseGenerated(DatabaseGeneratedOption.Identity)
        ]
        public int CategoryId { get; set; }

        [
            Required,
            Column("code"),
            NotNull,
            StringLength(6)
        ]
        public string? Code { get; set; }

        [
            Required,
            Column("title"),
            NotNull,
            StringLength(100,MinimumLength = 1)
        ]
        public string? Title { get; set; }

        [
            Required,
            Column("description"),
            AllowNull,
            StringLength(500)
        ]
        public string? Description { get; set; }

        [
            Column("date_created"),
            NotNull
        ]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public ICollection<Book>? Books { get; set; }
    }
}
