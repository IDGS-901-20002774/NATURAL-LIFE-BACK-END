using System.ComponentModel.DataAnnotations;

namespace NaturalLife.Model
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string? Correo { get; set; }
        public string? NombreRol { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Token { get; set; }
    }
}
