using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartQueue.Data;
using SmartQueue.Helpers;
using SmartQueue.Models;

namespace SmartQueue.Controllers
{
    [AllowAnonymous]
    public class CuentaController : Controller
    {
        private readonly SmartQueueContext _context;

        public CuentaController(SmartQueueContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel modelo)
        {
            if (!ModelState.IsValid)
            {
                return View(modelo);
            }

            // Buscar el usuario y comparar el hash de la contrasena
            string contrasenaHasheada = Seguridad.Hashear(modelo.Contrasena);
            Usuario usuario = _context.Usuarios
                .FirstOrDefault(u => u.Correo == modelo.Correo && u.Contrasena == contrasenaHasheada);

            if (usuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos. Verifique sus datos e intente nuevamente.";
                return View(modelo);
            }

            // Crear la cookie de sesion
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.Email, usuario.Correo)
            };
            var identidad = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identidad));

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
