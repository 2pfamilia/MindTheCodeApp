using AppCore.Models.BookModels;
using MindTheCodeApp.ViewModels.ShopVMs;

namespace MindTheCodeApp.ViewModels.ShopVMs
{
    public class ShopVM
    {
        public List<BookAuthor>? Authors { get; set; }
        public List<BookCategory>? Categories { get; set; }
        public decimal? MaxPrice { get; set; }
        public List<Book>? Books { get; set; }
    }
}
