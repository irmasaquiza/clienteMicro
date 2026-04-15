using System;
using System.Collections.Generic;
using System.Text;

namespace Microservicio.Clientes.DataAccess.Entities
{
    public class ClienteEntity
    {
        // IDENTIFICACIÓN TÉCNICA
        public int IdCliente { get; set; }
        public byte[] RowVersion { get; set; }

        // DATOS FUNCIONALES
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cedula { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }

        // ESTADO / CICLO DE VIDA
        public bool Estado { get; set; }

        // AUDITORÍA
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
        public string? UsuarioModificacion { get; set; }
        // BORRADO LÓGICO
        public bool Eliminado { get; set; }
    }
}
