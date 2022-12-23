using ProyectoFinalCoderHouse.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouse.Repositories
{
    public class UsuarioRepository
    {
        private SqlConnection? connection;
        String connectionString = "Data Source=.\\SQLEXPRESS; Initial Catalog = SistemaGestion; Integrated Security = True";
        //String connectionString = "Server=sql.bsite.net\\MSSQL2016;Database=mammary0743_coderdb;User Id=mammary0743_coderdb;Password=2XuMoYCSjd5oVZ;\r\n";
        public UsuarioRepository()
        {
            try
            {
                connection = new SqlConnection(connectionString);
            }
            catch (Exception)
            {
                throw new Exception("Connection with DB not established...");
            }
        }

        public List<Usuario>? ListUsers()
        {
            List<Usuario>? users = new List<Usuario>();
            if (connection == null) throw new Exception("Connection with DB not established...");

            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
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
                                        user.Id = int.Parse(reader["Id"].ToString());
                                        user.Nombre = reader["Nombre"].ToString();
                                        user.Apellido = reader["Apellido"].ToString();
                                        user.NombreUsuario = reader["NombreUsuario"].ToString();
                                        user.Contrasena = reader["Contraseña"].ToString();
                                        user.Mail = reader["Mail"].ToString();
                                        UserList.Add(user);
                                    }
                                    connection.Close();
                                }
                                else
                                {
                                    return null;
                                }
                                return UserList;
                            }
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public Usuario CreateUser(Usuario user)
        {
            if (connection == null)
            {
                throw new Exception("Connection with DB not established...");
            }
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
                    connection.Close();
                    return user;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public bool UpdateUsuario(Usuario usuario)
        {
            if (connection == null)
            {
                throw new Exception("Connection with DB not established...");
            }
            try
            {
                int rowsUpdated = 0;
                using (SqlCommand command = new SqlCommand("UPDATE Usuario SET Nombre = @nombre, Apellido = @apellido, NombreUsuario = @nombreUsuario, Contrasena = @contrasena, Mail = @mail WHERE id = @id", connection))
                {
                    connection.Open();
                    command.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = usuario.Nombre });
                    command.Parameters.Add(new SqlParameter("descripciones", SqlDbType.VarChar) { Value = usuario.Apellido });
                    command.Parameters.Add(new SqlParameter("costo", SqlDbType.Float) { Value = usuario.NombreUsuario });
                    command.Parameters.Add(new SqlParameter("precioVenta", SqlDbType.Float) { Value = usuario.Contrasena });
                    command.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = usuario.Mail });
                    command.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = usuario.Id });
                    rowsUpdated = command.ExecuteNonQuery();

                }
                connection.Close();
                return rowsUpdated > 0;
            }
            catch
            {
                throw;
            }
        }

        public bool eliminarUsuario(long Id)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Usuario WHERE Id = @Id", connection))
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.BigInt) { Value = Id });
                    filasAfectadas = cmd.ExecuteNonQuery();
                }
                connection.Close();
                return filasAfectadas > 0;
            }
            catch
            {
                throw;
            }
        }

    }
}
