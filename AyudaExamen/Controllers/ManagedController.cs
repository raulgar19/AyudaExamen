using Microsoft.AspNetCore.Mvc;

namespace AyudaExamen.Controllers
{
    public class ManagedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}