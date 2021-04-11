using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NET5.API.CURSODIOLocaliza.with.JWT.Models.Usuarios;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NET5.API.CURSODIOLocaliza.with.JWT.Configurations
{
    public class JWTService : IAuthenticationServiceCustom
    {
        private readonly IConfiguration _configuration; //Configuration vem do startup e é passado

        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GerarToken(UsuarioViewModelOutput usuarioViewModelOutput)
        {
            //Validação JWT
            var secret = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtConfigurations:Secret").Value);
            var symetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioViewModelOutput.Codigo.ToString()),
                    new Claim(ClaimTypes.Name, usuarioViewModelOutput.Login),
                    new Claim(ClaimTypes.Email, usuarioViewModelOutput.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            }; // Configura o Token

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(tokenGenerated);
        }
    }
}
