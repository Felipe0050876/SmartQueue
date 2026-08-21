using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartQueue.Data;

namespace SmartQueue.Controllers
{
    [Authorize]
    public class HistorialController : Controller
    {
        private readonly SmartQueueContext _context;

        public HistorialController(SmartQueueContext context)
        {
            _context = context;
        }

        // Solo lectura: turnos finalizados con busqueda por placa o nombre de cliente
        public IActionResult Index(string busqueda)
        {
            var consulta = _context.Turnos
                .Include(t => t.SolicitudAtencion)
                .Where(t => t.Estado == "Finalizado");

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                consulta = consulta.Where(t =>
                    t.SolicitudAtencion.Placa.Contains(busqueda) ||
                    t.SolicitudAtencion.NombreCliente.Contains(busqueda));
            }

            // Mostrar primero las atenciones finalizadas mas recientes
            var turnosFinalizados = consulta
                .OrderByDescending(t => t.FechaFinalizacion)
                .ToList();

            ViewBag.Busqueda = busqueda;
            return View(turnosFinalizados);
        }
    }
}
