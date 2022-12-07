using ProyectoFinalCoderHouse.Models;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouse.Repositories
{
    public class VentaRepository
    {
        private SqlConnection? connection;
        String connectionString = "Data Source=.\\SQLEXPRESS; Initial Catalog = SistemaGestion; Integrated Security = True";
        //String connectionString = "Server=sql.bsite.net\\MSSQL2016;Database=mammary0743_coderdb;User Id=mammary0743_coderdb;Password=2XuMoYCSjd5oVZ;\r\n";
        public VentaRepository()
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

        public List<Venta> listarVenta()
        {
            List<Venta> listaVenta = new List<Venta>();
            if (connection == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Venta", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Venta venta = obtenerVentaDesdeReader(reader);
                                listaVenta.Add(venta);
                            }
                        }
                    }
                }
                connection.Close();
            }
            catch
            {
                throw;
            }
            return listaVenta;
        }

        public Venta? obtenerVenta(long Id)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Venta WHERE Id = @Id", connection))
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = Id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Venta venta = obtenerVentaDesdeReader(reader);
                            return venta;
                        }
                        else
                        {
                            return null;
                        }
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

        public Venta? actualizarVenta(long id, Venta ventaAActualizar)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                Venta? venta = obtenerVenta(id);
                if (venta == null)
                {
                    return null;
                }
                List<string> camposAActualizar = new List<string>();
                if (venta.Comentarios != ventaAActualizar.Comentarios && !string.IsNullOrEmpty(ventaAActualizar.Comentarios))
                {
                    camposAActualizar.Add("comentarios = @comentarios");
                    venta.Comentarios = ventaAActualizar.Comentarios;
                }
                if (camposAActualizar.Count == 0)
                {
                    throw new Exception("No new fields to update");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE Venta SET {String.Join(", ", camposAActualizar)} WHERE id = @id", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("comentarios", SqlDbType.VarChar) { Value = ventaAActualizar.Comentarios });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return venta;
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

        public Venta crearVenta(Venta venta)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Venta(Comentarios, IdUsuario) " +
                    "VALUES(@comentarios, @idUsuario); SELECT @@Identity", connection))
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("comentarios", SqlDbType.VarChar) { Value = venta.Comentarios });
                    cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = venta.IdUsuario });
                    venta.Id = long.Parse(cmd.ExecuteScalar().ToString());
                    return venta;
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

        public bool eliminarVenta(long Id)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Venta WHERE Id = @Id", connection))
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

        private Venta obtenerVentaDesdeReader(SqlDataReader reader)
        {
            Venta venta = new Venta();
            venta.Id = long.Parse(reader["Id"].ToString());
            venta.Comentarios = reader["Comentarios"].ToString();
            venta.IdUsuario = long.Parse(reader["IdUsuario"].ToString());
            return venta;
        }
    }
}
