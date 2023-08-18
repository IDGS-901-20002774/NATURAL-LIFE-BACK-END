using System.ComponentModel.DataAnnotations;

namespace NaturalLife.Model
{
    public class MateriaPrima
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public decimal Cantidad { get; set; }
        [Required]
        public string UnidadMedida { get; set; }

    }
}
