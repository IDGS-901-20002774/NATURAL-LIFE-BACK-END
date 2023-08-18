using System.ComponentModel.DataAnnotations;

namespace NaturalLife.Model
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string NombreCliente { get; set; }
        [Required]
        public string ApellidosCliente { get; set; }
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string Correo { get; set; }
        [Required]
        public string Contrasenia { get; set; }
    }
}
