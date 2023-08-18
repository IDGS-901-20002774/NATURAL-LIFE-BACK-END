using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NaturalLife.Model;
using NaturalLife.Context;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NaturalLife.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;
        public LoginController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost]
        public ActionResult<Usuario> Post([FromBody] Login log)
        {
            try
            {
                SqlConnection conexion = (SqlConnection)_context.Database.GetDbConnection();
                SqlCommand comando = conexion.CreateCommand();
                conexion.Open();
                comando.CommandType = System.Data.CommandType.StoredProcedure;
                comando.CommandText = "sp_loginUsuario";

                comando.Parameters.Add("@Correo", System.Data.SqlDbType.VarChar).Value = log.correo;
                comando.Parameters.Add("@Contrasenia", System.Data.SqlDbType.VarChar).Value = log.contrasenia;

                SqlDataReader read = comando.ExecuteReader();
                if (read.Read())
                {
                    Usuario user = new Usuario();
                    user.Id = (int)read["UsuarioId"];
                    user.Correo = (string)read["Correo"];
                    user.NombreRol = (string)read["NombreRol"];
                    user.Nombre = (string)read["Nombre"];
                    user.Apellidos = (string)read["Apellidos"];
                    user.Telefono = (string)read["Telefono"];
                    user.Direccion = (string)read["Direccion"];

                    // Genera y agrega el token al objeto de usuario
                    var token = GenerarToken(user.Correo, user.NombreRol);
                    user.Token = token;

                    conexion.Close();
                    return Ok(user);
                }
                else
                {
                    conexion.Close();
                    return BadRequest("Datos incorrectos");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private string GenerarToken(string correo, string rol)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, correo),
                    new Claim(ClaimTypes.Role, rol)
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
