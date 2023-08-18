using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NaturalLife.Context;
using NaturalLife.Model;
using NaturalLife.Models;
using System.Net.NetworkInformation;

namespace NaturalLife.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaPrimaController : Controller
    {
        private readonly AppDbContext _context;

        public MateriaPrimaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{estatus}")]
        public ActionResult Get(int estatus)
        {
            try
            {
                List<MateriaPrima> listMateriaPrima = new List<MateriaPrima>();

                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_mostrarMateriaPrimaEstatus";
                comando.Parameters.AddWithValue("@Estatus", estatus);
                SqlDataReader read = comando.ExecuteReader();
                while (read.Read())
                {
                    MateriaPrima materia = new MateriaPrima();
                    materia.Id = (int)read["MateriaPrimaId"];
                    materia.Nombre = (string)read["Nombre"];
                    materia.Descripcion = (string)read["Descripcion"];
                    materia.Cantidad = (decimal)read["Cantidad"];
                    materia.UnidadMedida = (string)read["UnidadMedida"];
                    listMateriaPrima.Add(materia);
                }

                conexion.Close();
                return Json(listMateriaPrima);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<MateriaPrima> Post([FromBody] MateriaPrima materia)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_insertarMateriaPrima";

                comando.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar).Value = materia.Nombre;
                comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = materia.Descripcion;
                comando.Parameters.Add("@UnidadMedida", System.Data.SqlDbType.VarChar).Value = materia.UnidadMedida;
                comando.ExecuteReader();

                conexion.Close();

                return Ok(materia);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] MateriaPrima materia)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_actualizarMateriaPrima";

                comando.Parameters.Add("@Id", System.Data.SqlDbType.VarChar).Value = materia.Id;
                comando.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar).Value = materia.Nombre;
                comando.Parameters.Add("@Descripcion", System.Data.SqlDbType.VarChar).Value = materia.Descripcion;
                comando.Parameters.Add("@UnidadMedida", System.Data.SqlDbType.VarChar).Value = materia.UnidadMedida;
                comando.ExecuteReader();

                conexion.Close();

                return Ok(materia);
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
                comando.CommandText = "sp_eliminarMateriaPrima";
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
