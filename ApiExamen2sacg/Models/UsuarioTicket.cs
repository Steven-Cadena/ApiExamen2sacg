using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiExamen2sacg.Models
{
    [Table("USUARIOSTICKET")]
    public class UsuarioTicket
    {
        [Key]
        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }
        [Column("NOMBRE")]
        public string  Nombre { get; set; }
        [Column("APELLIDOS")]
        public string Apellidos { get; set; }
        [Column("EMAIL")]
        public string Email { get; set; }
        [Column("USERNAME")]
        public string Username { get; set; }
        [Column("PASSWORD")]
        public string Password { get; set; }
    }
}
