using ApiExamen2sacg.Helpers;
using ApiExamen2sacg.Models;
using ApiExamen2sacg.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiExamen2sacg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private RepositoryUsuarios repo;
        private HelperUsuarioToken helper;
        public LoginController(RepositoryUsuarios repo, HelperUsuarioToken helper) 
        {
            this.repo = repo;
            this.helper = helper;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult ValidarUsuario(LoginModel model)
        {

            UsuarioTicket usuario = 
                this.repo.ExisteUsuario(model.UserName, model.Password);

            if (usuario == null)
            {
                return Unauthorized();
            }
            else
            {
                //UN TOKEN CONTIENE UNAS CREDENCIALES
                SigningCredentials credentials =
                    new SigningCredentials(this.helper.GetKeyToken(), SecurityAlgorithms.HmacSha256);

                string jsonUsuario = JsonConvert.SerializeObject(usuario);
                Claim[] claims = new[]
                {
                new Claim("UserData", jsonUsuario)
                };


                JwtSecurityToken token =
                    new JwtSecurityToken(
                        claims: claims,
                        issuer: this.helper.Issuer,
                        audience: this.helper.Audience,
                        signingCredentials: credentials,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        notBefore: DateTime.UtcNow
                        );
                //DEVOLVEMOS UNA RESPUESTA CORRECTA CON EL TOKEN
                return Ok(
                    new
                    {
                        response =
                        new JwtSecurityTokenHandler().WriteToken(token)
                    });
            }
        }
    }
}
