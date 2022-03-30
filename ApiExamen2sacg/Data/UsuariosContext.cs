using ApiExamen2sacg.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExamen2sacg.Data
{
    public class UsuariosContext: DbContext
    {
        public UsuariosContext(DbContextOptions<UsuariosContext> options) : base(options) { }

        public DbSet<UsuarioTicket> Usuarios { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
