namespace MindTheCodeApp.ViewModels.BookVMs
{
    public class PhotoVM
    {
        public int PhotoId { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public IFormFile ImageFile { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsBook { get; set; } = false;
        public bool IsAuthor { get; set; } = false;
    }
}