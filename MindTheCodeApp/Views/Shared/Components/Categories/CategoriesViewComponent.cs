using AppCore.IRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;


namespace MindTheCodeApp.Views.Shared.Components.Categories;

public class CategoriesViewComponent : ViewComponent
{
    private readonly IBookRepository _bookRepository;

    public CategoriesViewComponent(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View(await _bookRepository.GetAllCategories());
    }

}

    