using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UniversityApiBackend.Models.JWTModels;

namespace UniversityApiBackend.Helpers
{
    public static class JwtHelpers
    {

        //Obtener Claims
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts, Guid Id)
        {
            var claims = new List<Claim>()
            {
                new Claim("Id", userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccounts.UserName),
                new Claim(ClaimTypes.Email, userAccounts.EmailId),
                new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                //Expiracion del token
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddMinutes(10).ToString("MMM ddd dd yyyy HH:mm:ss tt")),
                //Autenticacion por roles
                new Claim(ClaimTypes.Role, userAccounts.Rol.ToString())
        };

           
            
       

            return claims;

        }

        //En caso de que no pasaramos ID, para que autogenere uno.
        public static IEnumerable<Claim> GetClaims(this UserTokens userAccounts,out Guid Id)
        {
            Id= Guid.NewGuid();
            return GetClaims(userAccounts,Id);
        }

        //Generar tokens
        public static UserTokens GenTokenKey(UserTokens model, JwtSettings jwtSettings)
        {
            try
            {
                var userToken = new UserTokens();

                if(model == null)
                {
                    throw new ArgumentNullException(nameof(model));
                }

                //Obtain Secret Key
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);

                Guid id;

                //Tiempo de expiracion del token en 10 minutos

                DateTime expireTime = DateTime.Now.AddMinutes(10);

                //Validez del token

                userToken.Validity = expireTime.TimeOfDay;
                userToken.ExpiredTime = expireTime;

                //Generacion del JWT

                var jwt = new JwtSecurityToken(
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model, out id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: expireTime,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256
                        )
                    );

                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwt);
                userToken.Id= model.Id;
                userToken.UserName = model.UserName;
                userToken.GuidId = id;
                userToken.Rol = model.Rol;
                return userToken;

            }catch (Exception ex)
            {
                throw new Exception("Error generating the JWT", ex);
            }
        }


    }
}
