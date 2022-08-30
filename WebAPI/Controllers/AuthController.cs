using Business.Abstract;
using Core.Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public AuthController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO user)
        {
            try
            {
                _userServices.Register(user);
                return Ok(new { status = 200, mesage = "Ugurla qeydiyyatdan kecdiniz" });
            }
            catch(Exception c)
            {
                return BadRequest("Qeydiyyat zamani xeta bas verdi.Yeniden cehd et");
                
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            try
            {
                var user = _userServices.Login(loginDTO);
                return Ok(new {Status=200, message= user });
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        [Authorize]
        [HttpGet("getallUsers")]
        public IActionResult GetUsers()
        {
            var users = _userServices.GetAllUser();
            return Ok(new { status = 200, message = users });
        }
    }
}
