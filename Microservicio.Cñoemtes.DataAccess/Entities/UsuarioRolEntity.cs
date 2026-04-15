using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicio.Clientes.DataAccess.Entities
{
    public class UsuarioRolEntity
    {
        public int IdUsuario { get; set; }
        public UsuarioAppEntity? Usuario { get; set; }

        public int IdRol { get; set; }
        public RolEntity? Rol { get; set; }

        public DateTime? FechaAsignacion { get; set; }

        public string? Estado { get; set; }

        public DateTime FechaCreacion { get; set; }
        public string? IpCreacion { get; set; }
        public string? UsuarioCreacion { get; set; }
        public string? AccionCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public string? IpModificacion { get; set; }
        public string? AccionModificacion { get; set; }

        public bool Eliminado { get; set; }
    }
}