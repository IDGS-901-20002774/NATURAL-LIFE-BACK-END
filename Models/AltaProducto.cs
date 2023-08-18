using System.ComponentModel.DataAnnotations;

namespace NaturalLife.Models
{
    public class AltaProducto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Cantidad { get; set; }
    }
}
