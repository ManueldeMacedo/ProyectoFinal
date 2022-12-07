using ProyectoFinalCoderHouse.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouse.Repositories
{
    public class LoginRepository
    {
        private SqlConnection? connection;
        String connectionString = "Data Source=.\\SQLEXPRESS; Initial Catalog = SistemaGestion; Integrated Security = True";
        //String connectionString = "Server=sql.bsite.net\\MSSQL2016;Database=mammary0743_coderdb;User Id=mammary0743_coderdb;Password=2XuMoYCSjd5oVZ;\r\n";

        public LoginRepository()
        {
            try
            {
                connection = new SqlConnection(connectionString);
            }
            catch (Exception ex)
            {

            }
        }

        public bool verificarUsuario(Usuario usuario)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM usuario WHERE NombreUsuario = @NombreUsuario AND Contraseña = @Contrasenia", connection))
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("NombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("Contrasenia", SqlDbType.VarChar) { Value = usuario.Contrasena });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows;
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
    }
}
