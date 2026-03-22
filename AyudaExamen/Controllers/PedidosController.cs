using AyudaExamen.Filters;
using AyudaExamen.Helpers;
using AyudaExamen.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [AuthorizeUser(Policy = "CreatePedido")]
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

		[HttpGet]
		[AuthorizeUser(Policy = "CreatePedido")]
		public async Task<IActionResult> CreatePedido()
		{
			// Recuperar el id de usuario autenticado
			string? userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(userIdClaim))
			{
				// Si no hay usuario en claims, volvemos al carrito
				return RedirectToAction("GetCarritoSession", "Comics");
			}

			int usuarioId = int.Parse(userIdClaim);

			// Recuperar los cómics del carrito desde sesión
			List<int> comicIds = SessionHelper.GetCarrito(HttpContext.Session);
			if (comicIds == null || comicIds.Count == 0)
			{
				// Si no hay cómics en carrito, redirigimos a la vista del carrito
				return RedirectToAction("GetCarritoSession", "Comics");
			}

			int pedidoId = await this.repo.CreatePedido(usuarioId, comicIds);

			// Guardar el PedidoId en sesión para poder mostrarlo después
			HttpContext.Session.SetInt32(PEDIDO_Session_Key, pedidoId);

			// Limpiar el carrito una vez creado el pedido
			SessionHelper.LimpiarCarrito(HttpContext.Session);

			return RedirectToAction("Index");
		}

        public IActionResult LimpiarPedido()
        {
            HttpContext.Session.Remove(PEDIDO_Session_Key);
            return RedirectToAction("Index", "Comics");
        }
    }
}
