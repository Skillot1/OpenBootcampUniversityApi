using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UniversityApiBackend.Models.JWTModels;

namespace UniversityApiBackend
{
    public static class AddJwtTokenServicesExtensions
    {
        public static void AddJwtTokenServices(this IServiceCollection Services, IConfiguration configuration)
        {
            //Add JWT settings

            var bindJwtSettings = new JwtSettings();

            //Pasamos el valor del archivo appsettings.json a la variable bindJwtSettings
            configuration.Bind("JwtSettings", bindJwtSettings);

            //Add Singleton of JWT Settings
            Services.AddSingleton(bindJwtSettings);

            //Añadimos el proceso de autenticación

            Services.AddAuthentication(options =>
            {
                //Autenticar usuarios
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //Comprobar usuarios
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


            }).AddJwtBearer(options => {
            
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSingingKey,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),
                    ValidateIssuer = bindJwtSettings.ValidateIssuer,
                    ValidIssuer = bindJwtSettings.ValidIssuer,
                    ValidateAudience = bindJwtSettings.ValidateAudience,
                    ValidAudience = bindJwtSettings.ValidAudience,
                    RequireExpirationTime = bindJwtSettings.RequiredExpirationTime,
                    ValidateLifetime = bindJwtSettings.ValidateLifeTime,
                    ClockSkew = TimeSpan.FromMinutes(15)

                };

            });

        }
    }
}
