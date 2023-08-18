using System.ComponentModel.DataAnnotations;

namespace NaturalLife.Models
{
    public class Proceso
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NombreProceso { get; set; }
        [Required]
        public string Ingredientes { get; set; }
        [Required]
        public string Cantidades { get; set; }
        [Required]
        public string UnidadesMedida { get; set; }
    }
}
