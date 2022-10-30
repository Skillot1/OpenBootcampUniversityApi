using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public AccountsController(JwtSettings jwtSettings)
        {
            this.jwtSettings = jwtSettings;
        }

        //LOGIN momentaneamente será sin EF

        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id = 1,
                Email="prueba@gmail.com",
                Name ="Admin",
                Password= "Admin"

            },
             new User()
            {
                Id = 2,
                Email="prueba2@gmail.com",
                Name ="User1",
                Password= "user1"

            }

        };

        //Generacion del token
        [HttpPost]
        public IActionResult GetToken(UserLogins userLogin)
        {
            try
            {
                //Comprobamos validez del login
                bool valid = Logins.Any(user => user.Name == userLogin.UserName && userLogin.Password == userLogin.Password);

                if (!valid)
                {
                    return BadRequest("Wrong credentials");
                }

                //Seleccionamos al usuario
                var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));

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
            return Ok(Logins);
        }


    }
}
