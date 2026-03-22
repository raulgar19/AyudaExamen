using AyudaExamen.Models;
using AyudaExamen.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AyudaExamen.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryComics repo;

        public ManagedController(RepositoryComics repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            Usuario usuario = await this.repo.LogInUsuarioAsync(email);

            if (usuario != null)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role);

                if (usuario.Role == "admin")
                {
                    Claim claimAdmin = new Claim("Admin", "Soy el amo de la empresa");
                    identity.AddClaim(claimAdmin);
                }

                Claim claimName = new Claim(ClaimTypes.Name, usuario.Name);
                Claim claimId = new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString());
                Claim claimRole = new Claim(ClaimTypes.Role, usuario.Role);
                Claim claimEmail = new Claim("Email", usuario.Email);

                identity.AddClaim(claimName);
                identity.AddClaim(claimId);
                identity.AddClaim(claimRole);
                identity.AddClaim(claimEmail);

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal);

                string controller = TempData["CONTROLLER"].ToString();
                string action = TempData["ACTION"].ToString();

                if (TempData["id"] != null)
                {
                    string id = TempData["id"].ToString();

                    return RedirectToAction(action, controller, new { id = id });
                }
                else
                {
                    return RedirectToAction(action, controller);
                }
            }
            else
            {
                ViewData["MENSAJE"] = "Credenciales incorrectas";
                return View();
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult ErrorAcceso()
        {
            return View();
        }
    }
}