using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Text.Json;
using System.Data.SqlClient;
using ProyectoFinalCoderHouse.Models;
using ProyectoFinalCoderHouse.Repositories;

namespace ProyectoFinalCoderHouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {

        private UsuarioRepository repository = new UsuarioRepository();

        [EnableCors("AllowAnyOrigin")]
        [HttpGet]
        [Route("[action]")]

        public ActionResult<List<Usuario>> Get()
        {
            try
            {
                List<Usuario>? UserList = repository.ListUsers();
                return Ok(UserList);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [EnableCors("AllowAnyOrigin")]
        [HttpPost]
        [Route("[action]")]

        public ActionResult Post([FromBody] Usuario user)
        {
            try
            {
                Usuario createUser = repository.CreateUser(user);
                return StatusCode(StatusCodes.Status201Created, createUser);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [EnableCors("AllowAnyOrigin")]
        [HttpPut]
        [Route("[action]")]

        public ActionResult Put([FromBody] Usuario producto)
        {
            try
            {
                bool UpdateUsuario = repository.UpdateUsuario(producto);
                if (UpdateUsuario)
                {
                    return Ok("Usuario updated");
                }
                else return NotFound();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}