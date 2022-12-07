﻿using ProyectoFinalCoderHouse.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoFinalCoderHouse.Repositories
{
    public class ProductoRepository
    {
        private SqlConnection? connection;
        String connectionString = "Data Source=.\\SQLEXPRESS; Initial Catalog = SistemaGestion; Integrated Security = True";
        //String connectionString = "Server=sql.bsite.net\\MSSQL2016;Database=mammary0743_coderdb;User Id=mammary0743_coderdb;Password=2XuMoYCSjd5oVZ;\r\n";

        public ProductoRepository()
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

        public List<Producto> listarProducto()
        {
            List<Producto> listaProducto = new List<Producto>();
            if (connection == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Producto", connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Producto producto = obtenerProductoDesdeReader(reader);
                                listaProducto.Add(producto);
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
            return listaProducto;
        }

        public Producto? obtenerProducto(long Id)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Producto WHERE Id = @Id", connection))
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("Id", SqlDbType.BigInt) { Value = Id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Producto producto = obtenerProductoDesdeReader(reader);
                            return producto;
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

        public Producto crearProducto(Producto producto)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Producto(Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES(@descripcion, @costo, @precioVenta, @stock, @idUsuario); SELECT @@Identity", connection))
                {
                    connection.Open();
                    cmd.Parameters.Add(new SqlParameter("descripcion", SqlDbType.VarChar) { Value = producto.Descripcion });
                    cmd.Parameters.Add(new SqlParameter("costo", SqlDbType.Float) { Value = producto.Costo });
                    cmd.Parameters.Add(new SqlParameter("precioVenta", SqlDbType.Float) { Value = producto.PrecioVenta });
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = producto.Stock });
                    cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = producto.IdUsuario });
                    producto.Id = long.Parse(cmd.ExecuteScalar().ToString());
                    return producto;
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

        public Producto? actualizarProducto(long id, Producto productoAActualizar)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                Producto? producto = obtenerProducto(id);
                if (producto == null)
                {
                    return null;
                }
                List<string> camposAActualizar = new List<string>();
                if (producto.Descripcion != productoAActualizar.Descripcion && !string.IsNullOrEmpty(productoAActualizar.Descripcion))
                {
                    camposAActualizar.Add("descripciones = @descripcion");
                    producto.Descripcion = productoAActualizar.Descripcion;
                }
                if (producto.Costo != productoAActualizar.Costo && productoAActualizar.Costo > 0)
                {
                    camposAActualizar.Add("costo = @costo");
                    producto.Costo = productoAActualizar.Costo;
                }
                if (producto.PrecioVenta != productoAActualizar.PrecioVenta && productoAActualizar.PrecioVenta > 0)
                {
                    camposAActualizar.Add("precioVenta = @precioVenta");
                    producto.PrecioVenta = productoAActualizar.PrecioVenta;
                }
                if (producto.Stock != productoAActualizar.Stock && productoAActualizar.Stock > 0)
                {
                    camposAActualizar.Add("stock = @stock");
                    producto.Stock = productoAActualizar.Stock;
                }
                if (camposAActualizar.Count == 0)
                {
                    throw new Exception("No new fields to update");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE Producto SET {String.Join(", ", camposAActualizar)} WHERE id = @id", connection))
                {
                    cmd.Parameters.Add(new SqlParameter("descripcion", SqlDbType.VarChar) { Value = productoAActualizar.Descripcion });
                    cmd.Parameters.Add(new SqlParameter("costo", SqlDbType.Float) { Value = productoAActualizar.Costo });
                    cmd.Parameters.Add(new SqlParameter("precioVenta", SqlDbType.Float) { Value = productoAActualizar.PrecioVenta });
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = productoAActualizar.Stock });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    return producto;
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

        public bool eliminarProducto(long Id)
        {
            if (connection == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Producto WHERE Id = @Id", connection))
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

        private Producto obtenerProductoDesdeReader(SqlDataReader reader)
        {
            Producto producto = new Producto();
            producto.Id = long.Parse(reader["Id"].ToString());
            producto.Descripcion = reader["Descripciones"].ToString();
            producto.Costo = float.Parse(reader["Costo"].ToString());
            producto.PrecioVenta = float.Parse(reader["PrecioVenta"].ToString());
            producto.Stock = int.Parse(reader["Stock"].ToString());
            producto.IdUsuario = long.Parse(reader["IdUsuario"].ToString());
            return producto;
        }
    }
}
