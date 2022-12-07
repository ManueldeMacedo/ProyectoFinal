using ProyectoFinalCoderHouse.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Hosting;

namespace ProyectoFinalCoderHouse.Repositories
{
    public class ProductoVendidoRepository
    {
        private SqlConnection? connection;
        String connectionString = "Data Source=.\\SQLEXPRESS; Initial Catalog = SistemaGestion; Integrated Security = True";
        //String connectionString = "Server=sql.bsite.net\\MSSQL2016;Database=mammary0743_coderdb;User Id=mammary0743_coderdb;Password=2XuMoYCSjd5oVZ;\r\n";

        public ProductoVendidoRepository()
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

        public List<ProductoVendido> listarProductoVendido()
        {
            List<ProductoVendido> listaProductoVendido = new List<ProductoVendido>();
            if (connection == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ProductoVendido", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ProductoVendido productoVendido = obtenerProductoVendidoDesdeReader(reader);
                                listaProductoVendido.Add(productoVendido);
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
            return listaProductoVendido;
        }

        public ProductoVendido? obtenerProductoVendido(long Id)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ProductoVendido WHERE Id = @Id", connection))
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = Id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            ProductoVendido productoVendido = obtenerProductoVendidoDesdeReader(reader);
                            return productoVendido;
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

        public ProductoVendido crearProductoVendido(ProductoVendido productoVendido)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO ProductoVendido(Stock, IdProducto, IdVenta) " +
                    "VALUES(@stock, @idProducto, @idVenta); SELECT @@Identity", connection))
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = productoVendido.Stock });
                    cmd.Parameters.Add(new SqlParameter("idProducto", SqlDbType.Int) { Value = productoVendido.IdProducto });
                    cmd.Parameters.Add(new SqlParameter("idVenta", SqlDbType.Int) { Value = productoVendido.IdVenta });
                    productoVendido.Id = long.Parse(cmd.ExecuteScalar().ToString());
                    return productoVendido;
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

        public ProductoVendido? actualizarProductoVendido(long id, ProductoVendido productoVendidoAActualizar)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                ProductoVendido? productoVendido = obtenerProductoVendido(id);
                if (productoVendido == null)
                {
                    return null;
                }
                List<string> camposAActualizar = new List<string>();
                if (productoVendido.Stock != productoVendidoAActualizar.Stock && productoVendidoAActualizar.Stock > 0)
                {
                    camposAActualizar.Add("stock = @stock");
                    productoVendido.Stock = productoVendidoAActualizar.Stock;
                }
                if (productoVendido.IdProducto != productoVendidoAActualizar.IdProducto && productoVendidoAActualizar.IdProducto > 0)
                {
                    camposAActualizar.Add("idProducto = @idProducto");
                    productoVendido.IdProducto = productoVendidoAActualizar.IdProducto;
                }
                if (productoVendido.IdVenta != productoVendidoAActualizar.IdVenta && productoVendidoAActualizar.IdVenta > 0)
                {
                    camposAActualizar.Add("idVenta = @idVenta");
                    productoVendido.IdVenta = productoVendidoAActualizar.IdVenta;
                }
                if (camposAActualizar.Count == 0)
                {
                    throw new Exception("No new fields to update");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE Producto SET {String.Join(", ", camposAActualizar)} WHERE id = @id", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = productoVendidoAActualizar.Stock });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = productoVendidoAActualizar.IdProducto });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = productoVendidoAActualizar.IdVenta });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return productoVendido;
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

        public bool eliminarProductoVendido(long Id)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM ProductoVendido WHERE Id = @Id", connection))
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

        private ProductoVendido obtenerProductoVendidoDesdeReader(SqlDataReader reader)
        {
            ProductoVendido productoVendido = new ProductoVendido();
            productoVendido.Id = long.Parse(reader["Id"].ToString());
            productoVendido.Stock = int.Parse(reader["Stock"].ToString());
            productoVendido.IdProducto = long.Parse(reader["IdProducto"].ToString());
            productoVendido.IdVenta = long.Parse(reader["IdVenta"].ToString());
            return productoVendido;
        }
    }
}
