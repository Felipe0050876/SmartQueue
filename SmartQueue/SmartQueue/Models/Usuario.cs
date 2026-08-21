using System.ComponentModel.DataAnnotations;

namespace SmartQueue.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Correo { get; set; }

        // Se guarda el hash de la contrasena, no el texto plano
        [Required]
        public string Contrasena { get; set; }
    }
}
