using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartQueue.Models
{
    public class Turno
    {
        public int Id { get; set; }

        // codigo tipo A-001, A-002, se genera cuando se crea la solicitud
        public string Codigo { get; set; }

        // esperando, en servicio, finalizado
        public string Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaInicioAtencion { get; set; }

        public DateTime? FechaFinalizacion { get; set; }

        // relacion 1 a 1 con la solicitud que genero este turno
        public int SolicitudAtencionId { get; set; }

        [ForeignKey("SolicitudAtencionId")]
        public SolicitudAtencion SolicitudAtencion { get; set; }
    }
}