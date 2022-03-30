using ApiExamen2sacg.Data;
using ApiExamen2sacg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace ApiExamen2sacg.Repositories
{
    public class RepositoryUsuarios
    {
        private UsuariosContext context;
        private MediaTypeWithQualityHeaderValue Header;

        public RepositoryUsuarios(UsuariosContext context) 
        {
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
            this.context = context;
        }

        //metodo para obtener el id max de usuario
        private int GetMaxIdUsuario()
        {
            int id = 0;
            if (this.context.Usuarios.Count() > 0)
            {
                int max = this.context.Usuarios.Max(alumn => alumn.IdUsuario);
                id = max + 1;
                return id;
            }
            else
            {
                return 1;
            }
        }
        //mostrar los tickets de un usuario
        public List<Ticket> MostrarTicketsUsuario(int idusuario) 
        {
            var consulta = from datos in this.context.Tickets
                           where datos.IdUsuario == idusuario
                           select datos;
            return consulta.ToList();
        }

        //buscar ticket por su id
        public Ticket BuscarTicket(int idticket) 
        {
            return this.context.Tickets.Where(x => x.IdTicket == idticket).FirstOrDefault();
        }
        //metodo para comprobar si existe usuario
        public UsuarioTicket ExisteUsuario
            (string username, string password)
        {
            var consulta = from datos in this.context.Usuarios
                           where datos.Username == username
                           && datos.Password == password
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                return consulta.First();
            }
        }

        //metodo para crear un usuario nuevo
        public void InsertarUsuario(string nombre,string apellidos,string email,string username,string password) 
        {
            int id = GetMaxIdUsuario();
            UsuarioTicket usuario = new UsuarioTicket()
            {
                IdUsuario = id,
                Nombre = nombre,
                Apellidos = apellidos,
                Email = email,
                Username = username,
                Password = password
            };
            this.context.Usuarios.Add(usuario);
            this.context.SaveChanges();
        }

        public async Task CreateTicket(Ticket ticket) 
        {
            string urlFlowCreate =
                "https://prod-175.westeurope.logic.azure.com:443/workflows/8a7cc9d2a9e4499c86cba301fa711aed/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=tQoRjiDP4MIY6h9971NqykgMscibA0iJxKkx7Y27SG8";
            using (HttpClient client = new HttpClient()) 
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = JsonConvert.SerializeObject(ticket);
                StringContent content =
                    new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(urlFlowCreate, content);
            }
        }

        public async Task UpdateTicket(int idticket, string filename) 
        {
            string urlFlowUpdate =
                "https://prod-63.westeurope.logic.azure.com:443/workflows/8f57e6ca8e314d63a2a5a63e8ab45fbb/triggers/manual/paths/invoke?api-version=2016-06-01&sp=%2Ftriggers%2Fmanual%2Frun&sv=1.0&sig=eqGRaW4yfy3SN5zbKQAi2PoZ5OqGrobB_eHkWUl2x5E";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                //StringContent content =
                //    new StringContent(json, Encoding.UTF8, "application/json");
                //HttpResponseMessage response =
                //    await client.PostAsync(urlFlowUpdate, content);
            }
        }
    }
}
