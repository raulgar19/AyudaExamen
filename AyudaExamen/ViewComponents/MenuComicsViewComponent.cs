using AyudaExamen.Models;
using AyudaExamen.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AyudaExamen.ViewComponents
{
    public class MenuComicsViewComponent : ViewComponent
    {
        private RepositoryComics repo;

        public MenuComicsViewComponent(RepositoryComics repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Comic> comics = await this.repo.GetComicsAsync();
            return View(comics);
        }
    }
}
