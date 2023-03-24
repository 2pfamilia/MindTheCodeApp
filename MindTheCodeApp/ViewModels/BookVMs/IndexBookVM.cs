namespace MindTheCodeApp.ViewModels.BookVMs
{
    public class IndexBookVM
    {
        public int BookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int Count { get; set; }
        public string Author { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
