using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using AppCore.Models.BookModels;

namespace AppCore.Models.PhotoModels
{
    [Table("Photos")]
    public class Photo
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
            Required,
            Column("is_book"),
            NotNull,
        ]
        public bool IsBook { get; set; } = false;

        [
            Required,
            Column("is_author"),
            NotNull,
        ]
        public bool IsAuthor { get; set; } = false;

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
        public string? FilePath { get; set; }

        [
            Column("date_created"),
            NotNull
        ]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public ICollection<Book>? Books { get; set; }
        public ICollection<BookAuthor>? Authors { get; set; }
    }
}