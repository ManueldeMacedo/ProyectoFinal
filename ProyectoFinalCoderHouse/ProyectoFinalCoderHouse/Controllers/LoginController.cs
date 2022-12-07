using Microsoft.AspNetCore.Mvc;
using ProyectoFinalCoderHouse.Models;
using ProyectoFinalCoderHouse.Repositories;

namespace SistemaGestion.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : Controller
    {
        LoginRepository repository = new LoginRepository();

        [HttpPost]
        [Route("[action]")]
        public ActionResult<Usuario> Login(Usuario usuario)
        {
            try
            {
               bool usuarioExiste = repository.verificarUsuario(usuario);
                return usuarioExiste ? Ok() : NotFound();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
