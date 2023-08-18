using System.ComponentModel.DataAnnotations;

namespace NaturalLife.Model
{
    public class Proveedor
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string NombreProveedor { get; set; }
        [Required]
        public string MateriaSurte { get; set; }
        [Required]
        public string Telefono { get; set; }
    }
}
