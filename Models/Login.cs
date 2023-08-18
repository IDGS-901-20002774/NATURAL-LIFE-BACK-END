using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NaturalLife.Model
{
    [Keyless]
    public class Login
    {
        [Required]
        public string correo { get; set; }
        [Required]
        public string contrasenia { get; set; }
    }
}
