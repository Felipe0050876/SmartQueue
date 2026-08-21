using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartQueue.Data;

namespace SmartQueue.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly SmartQueueContext _context;

        public HomeController(SmartQueueContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Datos simples para el panel principal
            ViewBag.EnEspera = _context.Turnos.Count(t => t.Estado == "Esperando");
            ViewBag.EnServicio = _context.Turnos.Count(t => t.Estado == "En servicio");
            ViewBag.Finalizados = _context.Turnos.Count(t => t.Estado == "Finalizado");
            return View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}
