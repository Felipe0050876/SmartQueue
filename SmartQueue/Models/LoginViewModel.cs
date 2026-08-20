using System.ComponentModel.DataAnnotations;

namespace SmartQueue.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Ingrese su correo.")]
        [Display(Name = "Correo")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Ingrese su contrasena.")]
        [DataType(DataType.Password)]
        [Display(Name = "Contrasena")]
        public string Contrasena { get; set; }
    }
}
