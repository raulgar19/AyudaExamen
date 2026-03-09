using AyudaExamen.Models;
using AyudaExamen.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AyudaExamen.ViewComponents
{
    public class MenuHospitalesViewComponent : ViewComponent
    {
        private RepositoryHospital repo;

        public MenuHospitalesViewComponent(RepositoryHospital repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Hospital> hospitales = await this.repo.GetHospitales();

            return View(hospitales);
        }
    }
}