namespace MindTheCodeApp.ViewModels
{
    public class BookVM
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int BookCategoryId { get; set; }
        public int PhotoId { get; set; }
        public int Count { get; set; }
        public int BookAuthorId { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
