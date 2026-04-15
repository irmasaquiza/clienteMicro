using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicio.Clientes.DataAccess.Entities
{
    public class UsuarioAppEntity
    {
        // IDENTIFICACIÓN TÉCNICA
        public int IdUsuario { get; set; }
        public byte[] RowVersion { get; set; }

        // DATOS DE ACCESO
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        // RELACIÓN CON CLIENTE (opcional)
        public int? IdCliente { get; set; }
        public ClienteEntity Cliente { get; set; }

        // RELACIÓN CON ROLES 🔥 (MUCHOS A MUCHOS)
        public ICollection<UsuarioRolEntity> UsuarioRoles { get; set; }

        // SEGURIDAD
        public int IntentosFallidos { get; set; }
        public bool Bloqueado { get; set; }
        public DateTime? FechaUltimoLogin { get; set; }

        // ESTADO
        public string Estado { get; set; }

        // AUDITORÍA
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string? IpCreacion { get; set; }
        public string AccionCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        public string? IpModificacion { get; set; }
        public string? AccionModificacion { get; set; }

        // BORRADO LÓGICO
        public bool Eliminado { get; set; }
    }
}