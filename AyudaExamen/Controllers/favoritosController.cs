using AyudaExamen.Models;
using AyudaExamen.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AyudaExamen.Controllers
{
    public class favoritosController : Controller
    {
        private IMemoryCache _memoryCache;
        private RepositoryComics _repositoryComics;

        public favoritosController(IMemoryCache memoryCache, RepositoryComics repositoryComics)
        {
            _memoryCache = memoryCache;
            _repositoryComics = repositoryComics;
        }
        public IActionResult Index()
        {
            List<Comic> librosFavoritos;
            string cacheKey = "FAVORITOS";

            // Verificar si existe en caché
            if (_memoryCache.TryGetValue(cacheKey, out librosFavoritos))
            {
                return View(librosFavoritos);
            }

            // Si no existe en caché, devolver lista vacía
            librosFavoritos = new List<Comic>();
            return View(librosFavoritos);
        }
        public async Task<IActionResult> AddFavorito(int id)
        {
            List<Comic> librosFavoritos;
            string cacheKey = "FAVORITOS";

            // Obtener el Comic de la BD
            var comic = await _repositoryComics.FindComicAsync(id);

            if (comic == null)
            {
                TempData["Error"] = "El comic no existe.";
                return RedirectToAction("Index", "Comics");
            }

            // Verificar si existe en caché
            if (!_memoryCache.TryGetValue(cacheKey, out librosFavoritos))
            {
                librosFavoritos = new List<Comic>();
            }

            // Verificar que el comic no esté ya en favoritos
            if (!librosFavoritos.Any(l => l.Id == id))
            {
                librosFavoritos.Add(comic);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10));

                _memoryCache.Set(cacheKey, librosFavoritos, cacheOptions);

                TempData["Success"] = "Comic agregado a favoritos.";
            }
            else
            {
                TempData["Info"] = "El comic ya está en tus favoritos.";
            }

            return RedirectToAction("Index", "Comics");
        }
    }
}
