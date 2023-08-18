using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NaturalLife.Model;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Data.SqlTypes;
using NaturalLife.Context;
using NaturalLife.Models;

namespace NaturaLife.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : Controller
    {

        private readonly AppDbContext _context;
        public EmpleadoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                List<Empleado> listEmpleado = new List<Empleado>();

                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_mostrarEmpleadosActivos";
                SqlDataReader read = comando.ExecuteReader();
                while (read.Read())
                {
                    Empleado empleado = new Empleado();
                    empleado.Id = (int)read["EmpleadoId"];
                    empleado.NombreEmpleado = (string)read["NombreEmpleado"];
                    empleado.ApellidosEmpleado = (string)read["ApellidosEmpleado"];
                    empleado.Telefono = (string)read["Telefono"];
                    empleado.Direccion = (string)read["Direccion"];
                    listEmpleado.Add(empleado);
                }

                conexion.Close();
                return Json(listEmpleado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<Empleado> Post([FromBody] Empleado empleado)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_insertarEmpleado";

                comando.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar).Value = empleado.NombreEmpleado;
                comando.Parameters.Add("@Apellidos", System.Data.SqlDbType.VarChar).Value = empleado.ApellidosEmpleado;
                comando.Parameters.Add("@Telefono", System.Data.SqlDbType.VarChar).Value = empleado.Telefono;
                comando.Parameters.Add("@Direccion", System.Data.SqlDbType.VarChar).Value = empleado.Direccion;
                comando.ExecuteReader();

                conexion.Close();
                return Ok(empleado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
