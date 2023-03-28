namespace MindTheCodeApp.ViewModels.BookVMs
{
    public class CreateBookAuthorVM
    {
        public int AuthorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PhotoId { get; set; }
    }
}
