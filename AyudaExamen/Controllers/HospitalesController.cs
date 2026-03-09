using AyudaExamen.Models;
using AyudaExamen.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AyudaExamen.Controllers
{
    public class HospitalesController : Controller
    {
        private RepositoryHospital repo;

        public HospitalesController(RepositoryHospital repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Details(int? posicion, int idhospital)
        {
            if (posicion == null)
            {
                posicion = 1;
            }

            Hospital hospital = await this.repo.FindHospital(idhospital);
            return View(hospital);
        }
    }
}