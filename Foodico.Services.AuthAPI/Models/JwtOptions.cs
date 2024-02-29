namespace Foodico.Services.AuthAPI.Models
{
    public class JwtOptions
    {//Jwt token model options
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
    }
}
