using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;
using UniversityApiBackend.Models.JWTModels;
using UniversityApiBackend.Models.Logins;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly JwtSettings jwtSettings;
        private readonly UniversityDBContext context;
        private readonly ILogger<AccountsController> _logger;


        public AccountsController(JwtSettings jwtSettings, UniversityDBContext context, ILogger<AccountsController> _logger)
        {
            this.jwtSettings = jwtSettings;
            this.context = context;
            this._logger = _logger;
        }

   
        //Generacion del token
        [HttpPost]
        public  IActionResult GetToken(UserLogins userLogin)
        {
            try
            {

                //Comprobamos validez del login
                bool valid = context.Users.Any(user => user.Name == userLogin.UserName && user.Password == userLogin.Password);

                if (!valid)
                {
                    return BadRequest("Wrong credentials");
                }

                //Seleccionamos al usuario
                var user = context.Users.FirstOrDefault(user => user.Name.Equals(userLogin.UserName));

                //Generamos el token
                var Token = JwtHelpers.GenTokenKey
                      (new UserTokens()
                      {
                          EmailId = user.Email,
                          UserName = user.Name,
                          Id = user.Id,
                          GuidId = Guid.NewGuid(),
                          Rol = user.Rol
                      }, jwtSettings);

                //Devolvemos 200 y token
                return Ok(Token);

            }
            catch (Exception ex)
            {
                throw new Exception("GetToken Error", ex);
            }

        }

        /*EJEMPLO DE LLAMADA PARA CONSUMIR EN JS
        const data ={
  "userName": "Admin",
  "password": "Admin"
      }

const options = {
    method: 'POST',
  headers: {
      'Content-Type': 'application/json'
  },
  body:JSON.stringify(data)
};

        fetch('https://localhost:7143/api/Accounts/GetToken', options)
 .then(response => response.json())
  .then(data => console.log(data))
	.catch(err => console.error(err));
        */


        //Ejemplo de autenticacion para que solo pueda ver los logins el administrador

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult GetUserList(){
            return Ok(context.Users);
        }


    }
}
