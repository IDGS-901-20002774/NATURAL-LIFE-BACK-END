using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NaturalLife.Model;
using NaturalLife.Context;
using NaturalLife.Models;

namespace NaturalLife.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProveedorController : Controller
    {
        private readonly AppDbContext _context;
        public ProveedorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                List<Proveedor> listProveedor = new List<Proveedor>();

                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_mostrarProveedores";
                SqlDataReader read = comando.ExecuteReader();
                while (read.Read())
                {
                    Proveedor proveedor = new Proveedor();
                    proveedor.Id = (int)read["ProveedorId"];
                    proveedor.NombreProveedor = (string)read["NombreProveedor"];
                    proveedor.MateriaSurte = (string)read["MateriaSurte"];
                    proveedor.Telefono = (string)read["Telefono"];
                    listProveedor.Add(proveedor);
                }
                conexion.Close();
                return Json(listProveedor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Proveedor> Post([FromBody] Proveedor proveedor)
        {
            try
            {

                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_insertarProveedor";

                comando.Parameters.Add("@NombreProveedor", System.Data.SqlDbType.VarChar).Value = proveedor.NombreProveedor;
                comando.Parameters.Add("@MateriaSurte", System.Data.SqlDbType.VarChar).Value = proveedor.MateriaSurte;
                comando.Parameters.Add("@Telefono", System.Data.SqlDbType.VarChar).Value = proveedor.Telefono;
                comando.ExecuteReader();
                conexion.Close();

                return Ok(proveedor);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Proveedor proveedor)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_actualizarProveedor";

                comando.Parameters.Add("@Id", System.Data.SqlDbType.VarChar).Value = proveedor.Id;
                comando.Parameters.Add("@NombreProveedor", System.Data.SqlDbType.VarChar).Value = proveedor.NombreProveedor;
                comando.Parameters.Add("@MateriaSurte", System.Data.SqlDbType.VarChar).Value = proveedor.MateriaSurte;
                comando.Parameters.Add("@Telefono", System.Data.SqlDbType.VarChar).Value = proveedor.Telefono;
                comando.ExecuteReader();
                conexion.Close();

                return Ok(proveedor);
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
                comando.CommandText = "sp_eliminarProveedor";
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
