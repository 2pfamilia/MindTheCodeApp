using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MindTheCodeApp.Models.BookModels
{
    [Table("Books_Photos")]
    public class BookPhoto
    {
        [
            Key,
            Column("photo_id"),
            NotNull,
            DatabaseGenerated(DatabaseGeneratedOption.Identity)
        ]
        public int PhotoId { get; set; }

        [
            Required,
            Column("title"),
            NotNull,
            StringLength(100, MinimumLength = 1)
        ]
        public string? Title { get; set; }

        [
            Column("description"),
            AllowNull,
            StringLength(500)
        ]
        public string? Description { get; set; }

        [
            Required,
            Column("file"),
            NotNull
        ]
        public byte[]? File { get; set; }

        [
            Column("date_created"),
            NotNull
        ]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public ICollection<Book>? Books { get; set; }

    }
}
