using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiTienda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        /*private readonly IConfiguration Configuration;
        public SecurityController(IConfiguration configuration)
        {
            Configuration = configuration;
        }*/

        [HttpPost]
        [Route("autenticar")]
        public ClsTransaccion login(Usuario u)
        {
            ClsTransaccion rpta = new ClsTransaccion();
            try
            {
                LogginSoapClient client = new LogginSoapClient(LogginSoapClient.EndpointConfiguration.LogginSoap);
                var temp = client.AutenticarAsync(u.Identificacion, u.Password).Result;
                rpta = temp.Body.AutenticarResult;
                if (rpta.Codigo=="000")
                {
                    rpta.Mensaje = this.GenerateJSONWebToken(u);
                }
            }
            catch (Exception ex)
            {
                rpta.Codigo = "999";
                rpta.Mensaje = "Ocurrio un error " + ex.Message.ToString();
            }
            return rpta;
        }

        private string GenerateJSONWebToken(Usuario u)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Shiv@123333Shiv@12333"/*Configuration["MiLlavesh"]*/));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim("Issuer", "MiTienditapp"),//nobre del provedor(nombre de la app por ejemplo)
                //new Claim("Admin","true"),
                new Claim (JwtRegisteredClaimNames.UniqueName, u.Identificacion)
            };
            var token = new JwtSecurityToken("MiTienditapp",/*"MiTienditapp",*/null,claims,expires: DateTime.Now.AddSeconds(15), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
