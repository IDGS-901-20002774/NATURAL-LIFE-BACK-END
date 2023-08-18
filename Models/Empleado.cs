using System.ComponentModel.DataAnnotations;

namespace NaturalLife.Model
{
    public class Empleado
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NombreEmpleado { get; set; }
        [Required]
        public string ApellidosEmpleado{ get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Direccion { get; set; }
    }
}
