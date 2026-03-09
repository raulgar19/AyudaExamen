using AyudaExamen.Extensions;
using AyudaExamen.Models;
using AyudaExamen.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AyudaExamen.Controllers
{
    public class HospitalesController : Controller
    {
        private RepositoryHospital repo;
        private IMemoryCache cache;

        public HospitalesController(RepositoryHospital repo, IMemoryCache cache)
        {
            this.repo = repo;
            this.cache = cache;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? posicion, int idhospital)
        {
            Hospital hospitalSession;

            if (posicion == null)
            {
                posicion = 1;
            }

            int numeroRegistros = await this.repo.GetNumeroSalasHospitalAsync(idhospital);
            Hospital hospital = await this.repo.FindHospitalAsync(idhospital);
            List<Sala> salas = await this.repo.GetSalasHospitalAsync(posicion.Value, idhospital);

            SalasModel model = new SalasModel
            {
                Salas = salas,
                Registros = numeroRegistros,
                Hospital = hospital
            };

            ViewData["NUMEROREGISTROS"] = numeroRegistros;
            ViewData["POSICIONACTUAL"] = posicion.Value;

            if (HttpContext.Session.Get("hospital") != null)
            {
                hospitalSession = HttpContext.Session.GetObject<Hospital>("hospital");
            }
            else
            {
                hospitalSession = hospital;
                HttpContext.Session.SetObject("hospital", hospitalSession);
            }

            this.cache.Set($"hospital", hospital);

            return View(model);
        }

        public async Task<IActionResult> DetailsUnique(int? posicion, int idhospital)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            int numeroRegistros = await this.repo.GetNumeroSalasHospitalAsync(idhospital);
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

            Hospital hospital = await this.repo.FindHospitalAsync(idhospital);
            Sala sala = await this.repo.GetSalaByPosicionAsync(posicion.Value, idhospital);

            SalaUniqueModel model = new SalaUniqueModel
            {
                Sala = sala,
                Registros = numeroRegistros,
                Hospital = hospital
            };

            return View(model);
        }
    }
}