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
        public List<User> GetUsers()
        {
            return _userService.GetUsers();
        }

        [HttpGet("search/{id}")]
        public User? GetUser(int id)
        {
            return _userService.GetUser(id);
        }

        [HttpPost("create")]
        public IActionResult CreateUser(MCreateUser request)
        {
            int id = _userService.CreateUser(request);
            if (id ==0)
            {
                return BadRequest(new { message= "No se ha podido crear el usuario", success = false});
            }
            return Ok(new { message = $"Usuario creado exitosamente con id {id}", success = true });
        }

        [HttpPost("delete")]
        public IActionResult DeleteUser(MId request)
        {
            bool response = _userService.DeleteUser(request);
            if (!response)
            {
                return BadRequest(new { message = $"No se ha encontrado ningun usuario activo con id {request.Id}", success = false });
            }
            return Ok(new { message = $"Usuario con id {request.Id} eliminado exitosamente", success = true });
        }

        [HttpPost("update")]
        public IActionResult UpdateUser(MUpdateUser request)
        {
            var response = _userService.UpdateUser(request);
            if (!response)
            {
                return BadRequest(new { message = $"No se ha podido actualizar el usuario con id {request.Id}", success = false});
            }
            return Ok(new { message = $"Usuario con id {request.Id} actulizado correctamente", success = true });
        }
    }
}
