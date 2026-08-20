using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartQueue.Data;

namespace SmartQueue.Controllers
{
    [Authorize]
    public class TurnosController : Controller
    {
        private readonly SmartQueueContext _context;

        public TurnosController(SmartQueueContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Seccion = "turnos";

            // el turno que esta siendo atendido en este momento (solo puede haber uno)
            var turnoActivo = await _context.Turnos
                .Include(t => t.SolicitudAtencion)
                .FirstOrDefaultAsync(t => t.Estado == "En servicio");

            // los que estan esperando, del mas viejo al mas nuevo
            var enEspera = await _context.Turnos
                .Include(t => t.SolicitudAtencion)
                .Where(t => t.Estado == "Esperando")
                .OrderBy(t => t.FechaCreacion)
                .ToListAsync();

            ViewBag.TurnoActivo = turnoActivo;

            return View(enEspera);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LlamarSiguiente()
        {
            // no se puede llamar otro si ya hay uno en servicio
            bool hayTurnoActivo = await _context.Turnos.AnyAsync(t => t.Estado == "En servicio");

            if (hayTurnoActivo)
            {
                TempData["Error"] = "Ya hay un turno en servicio. Finalice el turno actual antes de llamar otro.";
                return RedirectToAction(nameof(Index));
            }

            var siguiente = await _context.Turnos
                .Where(t => t.Estado == "Esperando")
                .OrderBy(t => t.FechaCreacion)
                .FirstOrDefaultAsync();

            if (siguiente == null)
            {
                TempData["Error"] = "No hay turnos en espera.";
                return RedirectToAction(nameof(Index));
            }

            siguiente.Estado = "En servicio";
            siguiente.FechaInicioAtencion = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizarTurno(int id)
        {
            var turno = await _context.Turnos.FirstOrDefaultAsync(t => t.Id == id && t.Estado == "En servicio");

            if (turno == null)
            {
                TempData["Error"] = "El turno no existe o ya no esta en servicio.";
                return RedirectToAction(nameof(Index));
            }

            turno.Estado = "Finalizado";
            turno.FechaFinalizacion = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}