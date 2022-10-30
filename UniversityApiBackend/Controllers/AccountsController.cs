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

        public AccountsController(JwtSettings jwtSettings, UniversityDBContext context)
        {
            this.jwtSettings = jwtSettings;
            this.context = context;
        }

   
        //Generacion del token
        [HttpPost]
        public  IActionResult GetToken(UserLogins userLogin)
        {
            try
            {

                //Comprobamos validez del login
                bool valid = context.Users.Any(user => user.Name == userLogin.UserName && userLogin.Password == userLogin.Password);

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
                          GuidId = Guid.NewGuid()

                      }, jwtSettings);

                //Devolvemos 200 y token
                return Ok(Token);

            }
            catch (Exception ex)
            {
                throw new Exception("GetToken Error", ex);
            }

        }

        //Ejemplo de autenticacion para que solo pueda ver los logins el administrador

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult GetUserList(){
            return Ok(context.Users);
        }


    }
}
