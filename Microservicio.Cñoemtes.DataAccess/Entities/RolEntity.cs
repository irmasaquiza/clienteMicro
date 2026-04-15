using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicio.Clientes.DataAccess.Entities
{
    public class RolEntity
    {
        // IDENTIFICACIÓN TÉCNICA
        public int IdRol { get; set; }
        public byte[] RowVersion { get; set; }

        // DATOS FUNCIONALES
        public string Nombre { get; set; }   // ADMIN, CLIENTE, AEROLINEA
        public string Descripcion { get; set; }

        // RELACIÓN 🔥 (MUCHOS A MUCHOS)
        public ICollection<UsuarioRolEntity> UsuarioRoles { get; set; }

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