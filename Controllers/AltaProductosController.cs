using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NaturalLife.Context;
using NaturalLife.Model;
using NaturalLife.Models;

namespace NaturalLife.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AltaProductosController : Controller
    {
        private readonly AppDbContext _context;
        public AltaProductosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<Producto> Post([FromBody] AltaProducto producto)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_crearProducto";

                comando.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = producto.Id;
                comando.Parameters.Add("@CantidadProducto", System.Data.SqlDbType.VarChar).Value = producto.Cantidad;

                SqlParameter resultadoParam = new SqlParameter("@Resultado", System.Data.SqlDbType.VarChar, 255);
                resultadoParam.Direction = System.Data.ParameterDirection.Output;
                comando.Parameters.Add(resultadoParam);

                comando.ExecuteNonQuery();

                string mensajeResultado = resultadoParam.Value.ToString();

                conexion.Close();

                if (!string.IsNullOrEmpty(mensajeResultado))
                {
                    return Ok(mensajeResultado);
                }
                else
                {
                    return Ok(producto);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
