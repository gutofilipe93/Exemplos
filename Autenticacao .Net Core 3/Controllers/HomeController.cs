using System.Threading.Tasks;
using Autenticacao_.Net_Core_3.Models;
using Autenticacao_.Net_Core_3.Repositories;
using Autenticacao_.Net_Core_3.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Autenticacao_.Net_Core_3.Controllers
{
    [Route("v1/account")]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody] User model)
        {
            var user = UserRepository.Get(model.Username, model.Password);

            if(user == null)
                return NotFound(new {message = "Usuário ou senha incorretos"});

            var token = TokenService.GenerateToken(user);
            user.Password = string.Empty;
            return new
            {
                user = user,
                token = token
            };    
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";


        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => $"Autenticado -> {User.Identity.Name}";

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee, manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles ="manager")]
        public string Manager() => "Gerente";



    }
}