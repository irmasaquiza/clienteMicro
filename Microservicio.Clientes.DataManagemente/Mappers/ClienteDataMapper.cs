using System;
using System.Collections.Generic;
using System.Text;
using Microservicio.Clientes.DataAccess.Entities;
using Microservicio.Clientes.DataManagement.Models;

namespace Microservicio.Clientes.DataManagement.Mappers
{
    public static class ClienteDataMapper
    {
        // 🔄 ENTITY → DATA MODEL
        public static ClienteDataModel ToDataModel(ClienteEntity entity)
        {
            if (entity == null) return null;

            return new ClienteDataModel
            {
                IdCliente = entity.IdCliente,
                Nombre = entity.Nombre,
                Apellido = entity.Apellido,
                Cedula = entity.Cedula,
                Correo = entity.Correo,
                Telefono = entity.Telefono,
                Estado = entity.Estado,
                FechaCreacion = entity.FechaCreacion,
                UsuarioCreacion = entity.UsuarioCreacion,
                FechaModificacion = entity.FechaModificacion,
                UsuarioModificacion = entity.UsuarioModificacion,
                Eliminado = entity.Eliminado
            };
        }

        // 🔄 DATA MODEL → ENTITY
        public static ClienteEntity ToEntity(ClienteDataModel model)
        {
            if (model == null) return null;

            return new ClienteEntity
            {
                IdCliente = model.IdCliente,
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                Cedula = model.Cedula,
                Correo = model.Correo,
                Telefono = model.Telefono,
                Estado = model.Estado,
                Eliminado = model.Eliminado,
                UsuarioCreacion = model.UsuarioCreacion,
                FechaCreacion = model.FechaCreacion,
                UsuarioModificacion = model.UsuarioModificacion,
                FechaModificacion = model.FechaModificacion,


            };
        }

        // 🔄 UPDATE ENTITY DESDE MODEL (PRO 🔥)
        public static void UpdateEntity(ClienteEntity entity, ClienteDataModel model)
        {
            if (entity == null || model == null) return;

            entity.Nombre = model.Nombre;
            entity.Apellido = model.Apellido;
            entity.Cedula = model.Cedula;
            entity.Correo = model.Correo;
            entity.Telefono = model.Telefono;
            entity.Estado = model.Estado;
        }
    }
}