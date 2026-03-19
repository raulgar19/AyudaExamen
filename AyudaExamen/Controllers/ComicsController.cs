using AyudaExamen.Models;
using AyudaExamen.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AyudaExamen.Controllers
{
    public class ComicsController : Controller
    {
        private RepositoryComics repo;

        public ComicsController(RepositoryComics repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            List<Comic> comics = await this.repo.GetComicsAsync();

            return View(comics);
        }

        public async Task<IActionResult> Details(int id)
        {

            ComicImagesModel model = new ComicImagesModel();

            model.Comic = await this.repo.FindComicAsync(id);
            model.Registros = await this.repo.GetRegistrosAsync(id);


            return View(model);
        }
    }
}