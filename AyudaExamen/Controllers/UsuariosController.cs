using Microsoft.AspNetCore.Mvc;

namespace AyudaExamen.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}