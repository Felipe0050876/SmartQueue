using System.ComponentModel.DataAnnotations;

namespace SmartQueue.Models
{
    public class SolicitudAtencion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
        [Display(Name = "Nombre del cliente")]
        public string NombreCliente { get; set; }

        [Required(ErrorMessage = "La identificacion es obligatoria.")]
        [Display(Name = "Identificacion")]
        public string IdentificacionCliente { get; set; }

        [Required(ErrorMessage = "El telefono es obligatorio.")]
        [Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "La placa es obligatoria.")]
        [Display(Name = "Placa")]
        public string Placa { get; set; }

        [Required(ErrorMessage = "La marca es obligatoria.")]
        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "El modelo es obligatorio.")]
        [Display(Name = "Modelo")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "El año es obligatorio.")]
        [Range(1900, 2100, ErrorMessage = "Ingrese un año valido.")]
        [Display(Name = "Año")]
        public int Anio { get; set; }

        [Required(ErrorMessage = "El servicio solicitado es obligatorio.")]
        [Display(Name = "Servicio solicitado")]
        public string ServicioSolicitado { get; set; }

        public DateTime FechaCreacion { get; set; }

        // Turno asociado a esta solicitud (relacion 1 a 1)
        public Turno Turno { get; set; }
    }
}
