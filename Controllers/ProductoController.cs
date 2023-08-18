using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NaturalLife.Context;
using NaturalLife.Models;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace NaturalLife.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        private readonly AppDbContext _context;

        public ProductoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{estatus}")]
        public ActionResult Get(int estatus)
        {
            try
            {
                List<Producto> listProducto = new List<Producto>();

                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_mostrarProductosActivos";
                comando.Parameters.AddWithValue("@Estatus", estatus);
                SqlDataReader read = comando.ExecuteReader();
                while (read.Read())
                {
                    Producto producto = new Producto();
                    producto.Id = (int)read["ProductoId"];
                    producto.NombreProducto = (string)read["Nombre"];
                    producto.Costo = (decimal)read["Costo"];
                    producto.Descripcion = (string)read["Descripcion"];
                    producto.Imagen = (string)read["Imagen"];
                    producto.Cantidad = (int)read["CantidadProducto"];
                    producto.Ingredientes = (string)read["Ingredientes"];
                    producto.ProcesoId = (int)read["ProcesoId"];
                    listProducto.Add(producto);
                }

                conexion.Close();
                return Json(listProducto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Producto> Post([FromBody] Producto producto)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_insertarProducto";

                comando.Parameters.Add("@NombreProducto", System.Data.SqlDbType.VarChar).Value = producto.NombreProducto;
                comando.Parameters.Add("@CostoProducto", System.Data.SqlDbType.Decimal).Value = producto.Costo;
                comando.Parameters.Add("@ImagenProducto", System.Data.SqlDbType.VarChar).Value = producto.Imagen;
                comando.Parameters.Add("@DescripcionProducto", System.Data.SqlDbType.VarChar).Value = producto.Descripcion;
                comando.Parameters.Add("@ProcesoId", System.Data.SqlDbType.Int).Value = producto.ProcesoId;

                var resultadoParam = comando.Parameters.Add("@Resultado", System.Data.SqlDbType.VarChar, 255);
                resultadoParam.Direction = System.Data.ParameterDirection.Output;

                comando.ExecuteNonQuery();

                // Obtiene el valor del parámetro de salida
                string resultado = resultadoParam.Value.ToString();

                conexion.Close();

                if (resultado.Contains("registrado exitosamente"))
                {
                    return Ok(producto);
                }
                else
                {
                    return BadRequest(resultado);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Producto producto)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_actualizarProducto";

                comando.Parameters.Add("@Id", System.Data.SqlDbType.VarChar).Value = producto.Id;
                comando.Parameters.Add("@NombreProducto", System.Data.SqlDbType.VarChar).Value = producto.NombreProducto;
                comando.Parameters.Add("@CostoProducto", System.Data.SqlDbType.Decimal).Value = producto.Costo;
                comando.Parameters.Add("@DescripcionProducto", System.Data.SqlDbType.VarChar).Value = producto.Descripcion;
                comando.Parameters.Add("@ProcesoId", System.Data.SqlDbType.Int).Value = producto.ProcesoId;
                comando.ExecuteReader();

                conexion.Close();

                return Ok(producto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_eliminarProducto";
                comando.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;
                comando.ExecuteReader();
                conexion.Close();

                return Ok(id);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
