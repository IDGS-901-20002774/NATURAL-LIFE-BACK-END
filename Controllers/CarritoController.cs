using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NaturalLife.Context;
using NaturalLife.Models;

namespace NaturalLife.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : Controller
    {
        private readonly AppDbContext _context;
        public CarritoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                List<Carrito> imagenProducto = new List<Carrito>();

                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_mostrarImagenProducto";
                comando.Parameters.AddWithValue("@Id", id);
                SqlDataReader read = comando.ExecuteReader();
                while (read.Read())
                {
                    Carrito producto = new Carrito();
                    producto.Imagen = (string)read["Imagen"];
                    imagenProducto.Add(producto);
                }

                conexion.Close();
                return Json(imagenProducto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
