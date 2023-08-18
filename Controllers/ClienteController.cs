using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NaturalLife.Model;
using NaturalLife.Context;

namespace NaturaLife.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : Controller
    {
         private readonly AppDbContext _context;
          public ClienteController(AppDbContext context)
          {
              _context = context;

          }


        [HttpPost]
        public ActionResult<Cliente> Post([FromBody] Cliente cliente)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_insertarCliente";

                comando.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar).Value = cliente.NombreCliente;
                comando.Parameters.Add("@Apellidos", System.Data.SqlDbType.VarChar).Value = cliente.ApellidosCliente;
                comando.Parameters.Add("@Telefono", System.Data.SqlDbType.VarChar).Value = cliente.Telefono;
                comando.Parameters.Add("@Direccion", System.Data.SqlDbType.VarChar).Value = cliente.Direccion;
                comando.Parameters.Add("@Correo", System.Data.SqlDbType.VarChar).Value = cliente.Correo;
                comando.Parameters.Add("@Contrasenia", System.Data.SqlDbType.VarChar).Value = cliente.Contrasenia;
                comando.ExecuteReader();

                conexion.Close();
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
