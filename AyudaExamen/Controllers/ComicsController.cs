using AyudaExamen.Helpers;
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

        public async Task<IActionResult> DetailsSimple(int? posicion, int id)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            int numeroRegistros = await this.repo.GetRegistrosAsync(id);
            int siguiente = posicion.Value + 1;

            if (siguiente > numeroRegistros)
            {
                siguiente = numeroRegistros;
            }

            int anterior = posicion.Value - 1;

            if (anterior < 1)
            {
                anterior = 1;
            }

            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            ViewData["ULTIMO"] = numeroRegistros;
            ViewData["POSICION"] = posicion;

            Comic comic = await this.repo.FindComicAsync(id);

            Imagen imagen = await this.repo.GetImagenByPosicionAsync(id, posicion.Value);

            ComicImageSimpleModel model = new ComicImageSimpleModel
            {
                Registros = numeroRegistros,
                Comic = comic,
                Imagen = imagen
            };

            return View(model);
        }

        public async Task<IActionResult> DetailsComplejo(int id)
        {

            ComicImagesComplejoModel model = new ComicImagesComplejoModel();

            model.Comic = await this.repo.FindComicAsync(id);
            model.Registros = await this.repo.GetRegistrosAsync(id);


            return View(model);
        }
        public async Task<IActionResult> GetCarritoSession()
        {
            List<int> comicsId = SessionHelper.GetCarrito(HttpContext.Session);
            if (comicsId != null)
            {
                List<Comic> comics = new List<Comic>();
                foreach (int id in comicsId)
                {
                    Comic comic = await this.repo.FindComicAsync(id);

                    if (comic != null)
                    {
                        comics.Add(comic);

                    }
                }
                return View(comics);
            }
            else
            {
                return View();
            }

        }

        public async Task<IActionResult> AddToCarritoSesion(int id)
        {

            List<int> comicsId = SessionHelper.GetCarrito(HttpContext.Session);
            if (comicsId == null)
            {
                comicsId = new List<int>();
            }
            if (!comicsId.Contains(id))
            {
                comicsId.Add(id);
                SessionHelper.SetCarrito(HttpContext.Session, comicsId);
            }
            return RedirectToAction("Index");
        }
    }
}