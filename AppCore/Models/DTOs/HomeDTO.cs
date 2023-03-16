using AppCore.Models.BookModels;

namespace AppCore.Models.DTOs
{
    public class HomeDTO
    {
        public List<Book>? BestSellers { get; set; }
        public List<Book>? NewArrivals { get; set; }
        public List<BookAuthor>? Authors { get; set; }
    }
}
