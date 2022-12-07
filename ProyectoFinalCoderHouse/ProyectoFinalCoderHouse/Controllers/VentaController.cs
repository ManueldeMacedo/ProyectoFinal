using ProyectoFinalCoderHouse.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using System.Text.Json;

namespace ProyectoFinalCoderHouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : ControllerBase
    {

        [EnableCors("AllowAnyOrigin")]
        [HttpGet]
        [Route("getVentas")]

        public dynamic GetVentas()
        {
            String connectionString = "Data Source=.\\SQLEXPRESS; Initial Catalog = SistemaGestion; Integrated Security = True";
            //String connectionString = "Server=sql.bsite.net\\MSSQL2016;Database=mammary0743_coderdb;User Id=mammary0743_coderdb;Password=2XuMoYCSjd5oVZ;\r\n";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM Venta", connection))
                    {
                        connection.Open();
                        List<Venta> SellList = new List<Venta>();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Venta sell = new Venta();
                                    sell.Id = Int32.Parse(reader["Id"].ToString());
                                    sell.Comentarios = reader["Comentarios"].ToString();
                                    sell.IdUsuario = Int32.Parse(reader["IdUsuario"].ToString());

                                    SellList.Add(sell);
                                }
                                connection.Close();
                                var SellListJson = JsonSerializer.Serialize(SellList);
                                return SellListJson;
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
    }
}