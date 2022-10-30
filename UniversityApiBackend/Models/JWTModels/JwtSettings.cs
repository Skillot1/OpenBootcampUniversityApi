namespace UniversityApiBackend.Models.JWTModels
{
    public class JwtSettings
    {
        //Validacion firma
        public bool ValidateIssuerSingingKey { get; set; } //Si valida firma o no
        public string IssuerSigningKey { get; set; } //Salt

        //Validacion proveedor
        public bool ValidateIssuer { get; set; } = true; //Si valida emisor o no
        public string ValidIssuer { get; set; } //Ruta para validar

        //Validador audiencia
        public bool ValidateAudience { get; set; } = true; // Si valida audiencia o no
        public string ValidAudience { get; set; } //Quien pide la informacion (ruta)

        //Configuracion adicional
        public bool RequiredExpirationTime { get; set; } 
        public bool ValidateLifeTime { get; set; } = true;


    }
}
