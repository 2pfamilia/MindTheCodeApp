﻿using Microsoft.EntityFrameworkCore;
using AppCore.Models.OrderModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using AppCore.Models.PhotoModels;

namespace AppCore.Models.BookModels
{
    [Table("Books")]
    public class Book
    {
        [
            Key,
            Column("book_id"),
            NotNull,
            DatabaseGenerated(DatabaseGeneratedOption.Identity)
        ]
        public int BookId { get; set; }

        // maybe csv 
        //[
        //    Column("isbn")
        //]
        //public Guid ISBN { get; set; } = Guid.NewGuid();

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
            Column("category_id"),
            ForeignKey("category_id"),
            AllowNull
        ]
        public BookCategory? Category { get; set; }

        [
            Column("photo_id"),
            ForeignKey("photo_id"),
            AllowNull
        ]
        public Photo? Photo { get; set; }

        [
            Column("count")
        ]
        public int? Count { get; set; }

        [
            Column("author_id"),
            ForeignKey("author_id"),
            AllowNull
        ]
        public BookAuthor? Author { get; set; }

        [
            Required,
            Column("price"),
            NotNull,
            Precision(5, 2)
        ]
        public decimal? Price { get; set; }

        [
            Column("date_created"),
            NotNull
        ]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public ICollection<OrderDetails>? OrderDetails { get; set; }
    }
}