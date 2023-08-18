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
    public class ProcesoController : Controller
    {
        private readonly AppDbContext _context;

        public ProcesoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                List<Proceso> listProceso = new List<Proceso>();

                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_mostrarProcesosActivos";
                SqlDataReader read = comando.ExecuteReader();
                while (read.Read()) 
                {
                    Proceso proceso = new Proceso();
                    proceso.Id = (int)read["ProcesoId"];
                    proceso.NombreProceso = (string)read["NombreProceso"];
                    proceso.Ingredientes = (string)read["Ingredientes"];
                    listProceso.Add(proceso);
                }

                conexion.Close();
                return Json(listProceso);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                List<Proceso> listProceso = new List<Proceso>();

                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_mostrarProcesoPorId"; 
                comando.Parameters.AddWithValue("@Id", id); 
                SqlDataReader read = comando.ExecuteReader();

                while (read.Read())
                {
                    Proceso proceso = new Proceso();
                    proceso.Id = (int)read["ProcesoId"];
                    proceso.NombreProceso = (string)read["NombreProceso"];
                    proceso.Ingredientes = (string)read["Ingredientes"];
                    proceso.Cantidades = (string)read["Cantidades"];
                    proceso.UnidadesMedida = (string)read["UnidadesMedida"];
                    listProceso.Add(proceso);
                }

                conexion.Close();

                if (listProceso.Count == 0)
                {
                    return NotFound();
                }

                return Json(listProceso);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Proceso> Post([FromBody] Proceso proceso)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_insertarProceso";

                comando.Parameters.Add("@NombreProceso", System.Data.SqlDbType.VarChar).Value = proceso.NombreProceso;
                comando.Parameters.Add("@Ingredientes", System.Data.SqlDbType.VarChar).Value = proceso.Ingredientes;
                comando.Parameters.Add("@Cantidades", System.Data.SqlDbType.VarChar).Value = proceso.Cantidades;
                comando.Parameters.Add("@UnidadesMedida", System.Data.SqlDbType.VarChar).Value = proceso.UnidadesMedida;
                comando.ExecuteReader();

                conexion.Close();

                return Ok(proceso);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Proceso proceso)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_actualizarProceso";

                comando.Parameters.Add("@Id", System.Data.SqlDbType.VarChar).Value = proceso.Id;
                comando.Parameters.Add("@NombreProceso", System.Data.SqlDbType.VarChar).Value = proceso.NombreProceso;
                comando.Parameters.Add("@Ingredientes", System.Data.SqlDbType.VarChar).Value = proceso.Ingredientes;
                comando.Parameters.Add("@Cantidades", System.Data.SqlDbType.VarChar).Value = proceso.Cantidades;
                comando.Parameters.Add("@UnidadesMedida", System.Data.SqlDbType.VarChar).Value = proceso.UnidadesMedida;
                comando.ExecuteReader();

                conexion.Close();

                return Ok(proceso);
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
                comando.CommandText = "sp_eliminarProceso";
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
