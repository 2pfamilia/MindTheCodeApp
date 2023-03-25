namespace MindTheCodeApp.ViewModels.BookVMs
{
    public class EditBookCategoryVM
    {
        public int EditBookCategoryId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}