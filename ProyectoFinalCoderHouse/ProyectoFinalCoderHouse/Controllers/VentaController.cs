using ProyectoFinalCoderHouse.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Security.Cryptography;
using System.Text.Json;
using ProyectoFinalCoderHouse.Repositories;

namespace ProyectoFinalCoderHouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : ControllerBase
    {
        private VentaRepository repository = new VentaRepository();

        [EnableCors("AllowAnyOrigin")]
        [HttpGet]
        [Route("[action]")]

        public ActionResult<List<Venta>> Get()
        {
            try
            {
                List<Venta>? VentaList = repository.listarVenta();
                return Ok(VentaList);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpGet("{Id}")]

        public ActionResult<Venta> Get(long Id)
        {
            try
            {
                Venta? Venta = repository.obtenerVenta(Id);
                return Ok(Venta);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Venta> Put(long id, [FromBody] Venta ventaAActualizar)
        {
            try
            {
                Venta? ventaActualizado = repository.actualizarVenta(id, ventaAActualizar);
                if (ventaActualizado != null)
                {
                    return Ok(ventaActualizado);
                }
                else
                {
                    return NotFound("la venta no fue encontrada");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Venta venta)
        {
            try
            {
                Venta ventaCreado = repository.crearVenta(venta);
                return StatusCode(StatusCodes.Status201Created, ventaCreado);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult Delete([FromBody] long Id)
        {
            try
            {
                bool seElimino = repository.eliminarVenta(Id);
                if (seElimino)
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

    }
}