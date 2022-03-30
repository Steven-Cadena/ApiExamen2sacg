using ApiExamen2sacg.Models;
using ApiExamen2sacg.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiExamen2sacg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private RepositoryUsuarios repo;
        public TicketsController(RepositoryUsuarios repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public ActionResult CreateAlumno(UsuarioTicket usuario)
        {
            //List<Claim> claims = HttpContext.User.Claims.ToList();

            //string jsonAlumno = claims.SingleOrDefault(z => z.Type == "UserData").Value;

            //UsuarioTicket usuarioticket = JsonConvert.DeserializeObject<UsuarioTicket>(jsonAlumno);

            this.repo.InsertarUsuario(usuario.Nombre, usuario.Apellidos, usuario.Email, usuario.Username, usuario.Password);
            return Ok();
        }

        [HttpGet]
        [Authorize]
        [Route("[action]/{id}")]
        public ActionResult<Ticket> FindTicket(int id)
        {
            List<Claim> claims = HttpContext.User.Claims.ToList();
            string jsonTicket = claims.SingleOrDefault(z => z.Type == "UserData").Value;
            Ticket ticket = JsonConvert.DeserializeObject<Ticket>(jsonTicket);
            return ticket;
        }
        [HttpGet]
        [Authorize]
        [Route("[action]/{id}")]
        public ActionResult<List<Ticket>> TicketsUsuario(int id) 
        {
            return this.repo.MostrarTicketsUsuario(id);
        }

        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public async Task<ActionResult> CreateTicket(Ticket ticket) 
        {
            await this.repo.CreateTicket(ticket);
            return Ok();
        }


    }
}
