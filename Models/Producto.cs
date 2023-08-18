using System.ComponentModel.DataAnnotations;

namespace NaturalLife.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? NombreProducto { get; set; }
        [Required]
        public decimal Costo { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string Imagen { get; set; }
        [Required]
        public int Cantidad { get; set; }
        public string? Ingredientes { get; set; }
        [Required]
        public int ProcesoId { get; set; }
    }
}
