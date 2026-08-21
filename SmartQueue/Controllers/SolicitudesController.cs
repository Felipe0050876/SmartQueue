using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartQueue.Data;
using SmartQueue.Models;

namespace SmartQueue.Controllers
{
    [Authorize]
    public class SolicitudesController : Controller
    {
        private readonly SmartQueueContext _context;

        public SolicitudesController(SmartQueueContext context)
        {
            _context = context;
        }

        // Lista de solicitudes registradas
        public IActionResult Index()
        {
            var solicitudes = _context.Solicitudes
                .Include(s => s.Turno)
                .OrderByDescending(s => s.FechaCreacion)
                .ToList();
            return View(solicitudes);
        }

        // Detalle de una solicitud
        public IActionResult Details(int id)
        {
            var solicitud = _context.Solicitudes
                .Include(s => s.Turno)
                .FirstOrDefault(s => s.Id == id);

            if (solicitud == null)
            {
                return NotFound();
            }
            return View(solicitud);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SolicitudAtencion solicitud)
        {
            if (!ModelState.IsValid)
            {
                return View(solicitud);
            }

            solicitud.FechaCreacion = DateTime.Now;
            _context.Solicitudes.Add(solicitud);
            _context.SaveChanges();

            // Generar automaticamente el turno de la nueva solicitud
            Turno turno = new Turno
            {
                Codigo = GenerarCodigoTurno(),
                Estado = "Esperando",
                FechaCreacion = DateTime.Now,
                SolicitudAtencionId = solicitud.Id
            };
            _context.Turnos.Add(turno);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var solicitud = _context.Solicitudes.Find(id);
            if (solicitud == null)
            {
                return NotFound();
            }
            return View(solicitud);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, SolicitudAtencion solicitud)
        {
            if (!ModelState.IsValid)
            {
                return View(solicitud);
            }

            var existente = _context.Solicitudes.Find(id);
            if (existente == null)
            {
                return NotFound();
            }

            existente.NombreCliente = solicitud.NombreCliente;
            existente.IdentificacionCliente = solicitud.IdentificacionCliente;
            existente.Telefono = solicitud.Telefono;
            existente.Placa = solicitud.Placa;
            existente.Marca = solicitud.Marca;
            existente.Modelo = solicitud.Modelo;
            existente.Anio = solicitud.Anio;
            existente.ServicioSolicitado = solicitud.ServicioSolicitado;

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var solicitud = _context.Solicitudes
                .Include(s => s.Turno)
                .FirstOrDefault(s => s.Id == id);

            if (solicitud == null)
            {
                return NotFound();
            }
            return View(solicitud);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var solicitud = _context.Solicitudes.Find(id);
            if (solicitud != null)
            {
                // El turno asociado se elimina en cascada
                _context.Solicitudes.Remove(solicitud);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // Generar el siguiente codigo consecutivo con formato A-001
        private string GenerarCodigoTurno()
        {
            int siguienteNumero = 1;
            var turnos = _context.Turnos.ToList();

            if (turnos.Count > 0)
            {
                int maximo = 0;
                foreach (var turno in turnos)
                {
                    string parteNumerica = turno.Codigo.Replace("A-", "");
                    if (int.TryParse(parteNumerica, out int numero) && numero > maximo)
                    {
                        maximo = numero;
                    }
                }
                siguienteNumero = maximo + 1;
            }

            return "A-" + siguienteNumero.ToString("000");
        }
    }
}
