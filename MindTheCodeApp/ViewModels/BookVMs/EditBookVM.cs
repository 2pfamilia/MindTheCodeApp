namespace MindTheCodeApp.ViewModels.BookVMs
{
    public class EditBookVM
    {
        public int EditBookId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public int PhotoId { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
    }
}