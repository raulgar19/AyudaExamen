using AyudaExamen.Extensions;
using AyudaExamen.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AyudaExamen.Controllers
{
    public class PedidosController : Controller
    {
        private RepositoryComics repo;
        private const string PEDIDO_Session_Key = "PedidoId";

        public PedidosController(RepositoryComics repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            int? pedidoId = HttpContext.Session.GetInt32(PEDIDO_Session_Key);
            if (!pedidoId.HasValue)
            {
                return RedirectToAction("Index", "Comics");
            }

            var pedidoInfo = await this.repo.GetPedidoInfo(pedidoId.Value);
            if (pedidoInfo == null || pedidoInfo.Comic == null || pedidoInfo.Comic.Count == 0)
            {
                return RedirectToAction("Index", "Comics");
            }

            return View(pedidoInfo);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePedido(int usuarioId, List<int> comicIds)
        {
            if (comicIds == null || comicIds.Count == 0)
            {
                return BadRequest("Debe seleccionar al menos un cómic");
            }

            try
            {
                int pedidoId = await this.repo.CreatePedido(usuarioId, comicIds);
                
                // Guardar el PedidoId en sesión
                HttpContext.Session.SetInt32(PEDIDO_Session_Key, pedidoId);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear el pedido: {ex.Message}");
            }
        }

        public IActionResult LimpiarPedido()
        {
            HttpContext.Session.Remove(PEDIDO_Session_Key);
            return RedirectToAction("Index", "Comics");
        }
    }
}
