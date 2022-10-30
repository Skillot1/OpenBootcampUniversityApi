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
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddMinutes(15).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };

            if(userAccounts.UserName == "Admin")
            {
             claims.Add(new Claim(ClaimTypes.Role, "Administrator")); 
            }
            else if(userAccounts.UserName == "User 1" )
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("UserOnly", "User1"));
            }

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

                //Tiempo de expiracion del token en 15 minutos

                DateTime expireTime = DateTime.UtcNow.AddMinutes(15);

                //Validez del token

                userToken.Validity = expireTime.TimeOfDay;

                //Generacion del JWT

                var jwt = new JwtSecurityToken(
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model, out id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256
                        )
                    );

                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwt);
                userToken.Id= model.Id;
                userToken.UserName = model.UserName;
                userToken.GuidId = id;
                return userToken;

            }catch (Exception ex)
            {
                throw new Exception("Error generating the JWT", ex);
            }
        }


    }
}
