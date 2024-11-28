using API.Services;
using DAO_Entidades;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {

        private UserService _userService;
        public UsersController()
        {
            _userService = new UserService();
        }

        [HttpGet("search/all")]
        public IEnumerable<User> GetUsers()
        {
            return _userService.GetUsers();
        }

        [HttpGet("search/{id}")]
        [ProducesResponseType<User>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser(int id)
        {
            var user = _userService.GetUser(id);
            return user == null ? 
                NotFound(new { message = $"No se ha encontrado el usuario con id {id}", success=false })
                : Ok(user);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateUser(MCreateUser request)
        {
            int id = _userService.CreateUser(request);
            return id == 0 ?
                BadRequest(new { message = "No se ha podido crear el usuario", success = false })
                : Ok(new { message = $"Usuario creado exitosamente con id {id}", success = true });
        }

        [HttpPost("delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser(MId request)
        {
            bool response = _userService.DeleteUser(request);
            return response ?
                Ok(new { message = $"Usuario con id {request.Id} eliminado exitosamente", success = true })
                : NotFound(new { message = $"No se ha encontrado ningun usuario activo con id {request.Id}", success = false });
        }

        [HttpPost("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateUser(MUpdateUser request)
        {
            bool response = _userService.UpdateUser(request);
            return response ?
                Ok(new { message = $"Usuario con id {request.Id} actulizado correctamente", success = true })
                : BadRequest(new { message = $"No se ha podido actualizar el usuario con id {request.Id}", success = false});
        }
    }
}
