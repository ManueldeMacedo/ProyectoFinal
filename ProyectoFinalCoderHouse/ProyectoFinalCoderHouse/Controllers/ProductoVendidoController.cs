using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Data.SqlClient;
using ProyectoFinalCoderHouse.Models;

namespace ProyectoFinalCoderHouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoVendidoController : ControllerBase
    {
        private readonly ILogger<ProductoVendidoController> _logger;

        public ProductoVendidoController(ILogger<ProductoVendidoController> logger)
        {
            _logger = logger;
        }

        [EnableCors("AllowAnyOrigin")]
        [HttpGet]
        [Route("getallsoldproducts")]

        public dynamic GetSoldProducts()
        {
            String connectionString = "Data Source=.\\SQLEXPRESS; Initial Catalog = SistemaGestion; Integrated Security = True";
            //String connectionString = "Server=sql.bsite.net\\MSSQL2016;Database=mammary0743_coderdb;User Id=mammary0743_coderdb;Password=2XuMoYCSjd5oVZ;\r\n";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("SELECT * FROM ProductoVendido", connection))
                    {
                        connection.Open();
                        List<ProductoVendido> SoldProductList = new List<ProductoVendido>();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    ProductoVendido productSold = new ProductoVendido();
                                    productSold.Id = int.Parse(reader["Id"].ToString());
                                    productSold.Stock = int.Parse(reader["Stock"].ToString());
                                    productSold.IdProducto = int.Parse(reader["IdProducto"].ToString());
                                    productSold.IdVenta = int.Parse(reader["IdVenta"].ToString());

                                    SoldProductList.Add(productSold);
                                }
                                connection.Close();
                                var SoldProductListJson = JsonSerializer.Serialize(SoldProductList);
                                return SoldProductListJson;
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