using AppCore.Models.BookModels;
using MindTheCodeApp.ViewModels.AuthorVMs;

namespace MindTheCodeApp.ViewModels.AuthorVMs
{
    public class AuthorDetailsVM
    {
        public BookAuthor? Author { get; set; }
        public List<Book>? Books { get; set; }
    }
}
