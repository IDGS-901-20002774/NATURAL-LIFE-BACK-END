using System.ComponentModel.DataAnnotations;

namespace NaturalLife.Model
{
    public class Rol
    {
        [Key]
        public int id_rol { get; set; }
        public string tipo { get; set; }
        public string descripcion { get; set; }
       
    }
}
