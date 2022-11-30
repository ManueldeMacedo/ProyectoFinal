using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.Json;
using System.Data.SqlClient;
using ProyectoFinalCoderHouse.Models;

namespace ProyectoFinalCoderHouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly ILogger<VentaController> _logger;

        public UsuarioController(ILogger<VentaController> logger)
        {
            _logger = logger;
        }

        [EnableCors("AllowAnyOrigin")]
        [HttpGet]
        [Route("getallusers")]
        public dynamic GetUsers()
        {

            String connectionString = "Data Source=.\\SQLEXPRESS; Initial Catalog = SistemaGestion; Integrated Security = True";
            //String connectionString = "Server=sql.bsite.net\\MSSQL2016;Database=mammary0743_coderdb;User Id=mammary0743_coderdb;Password=2XuMoYCSjd5oVZ;\r\n";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Usuario", connection))
                    {
                        connection.Open();
                        List<Usuario> UserList = new List<Usuario>();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Usuario user = new Usuario();
                                    user.Id = int.Parse(reader["id"].ToString());
                                    user.Nombre = reader["Nombre"].ToString();
                                    user.Apellido = reader["Apellido"].ToString();
                                    user.NombreUsuario = reader["NombreUsuario"].ToString();
                                    user.Contrasena = reader["Contraseña"].ToString();
                                    user.Mail = reader["Mail"].ToString();

                                    UserList.Add(user);
                                }
                                connection.Close();
                                var UserListJson = JsonSerializer.Serialize(UserList);
                                return UserListJson;
                            }
                            else
                            {
                                return "No data";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }

        }

        [EnableCors("AllowAnyOrigin")]
        [HttpPost]
        [Route("createuser")]
        public dynamic CreateUser(Usuario user)
        {
            String connectionString = "Data Source=.\\SQLEXPRESS; Initial Catalog = SistemaGestion; Integrated Security = True";
            //String connectionString = "Server=sql.bsite.net\\MSSQL2016;Database=mammary0743_coderdb;User Id=mammary0743_coderdb;Password=2XuMoYCSjd5oVZ;\r\n";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("INSERT INTO Usuario VALUES (@nombre, @apellido, @nombreUsuario, @contrasena, @mail)", connection))
                    {
                        connection.Open();
                        command.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = user.Nombre });
                        command.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = user.Apellido });
                        command.Parameters.Add(new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = user.NombreUsuario });
                        command.Parameters.Add(new SqlParameter("contrasena", SqlDbType.VarChar) { Value = user.Contrasena });
                        command.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = user.Mail });

                        var InsertNewUser = command.ExecuteNonQuery();
                        return InsertNewUser;
                    }

                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }
    }
}