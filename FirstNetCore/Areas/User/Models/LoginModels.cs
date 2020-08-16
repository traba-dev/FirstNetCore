using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FirstNetCore.Areas.User.Models
{
    public class LoginModels
    {
        [Required(ErrorMessage = "El campo Correo Electrónico es requerido")]
        [EmailAddress(ErrorMessage = "El correo no es un formato de correo válido")]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo Contraseña es requerido")]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        [StringLength(100, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 6)]
        public string Password { get; set; }

        public string Error { get; set; }
    }
}
