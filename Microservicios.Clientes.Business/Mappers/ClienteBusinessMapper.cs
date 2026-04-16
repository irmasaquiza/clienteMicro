using System;
using System.Collections.Generic;
using System.Text;

using Microservicio.Clientes.Business.DTOs;
using Microservicio.Clientes.DataManagement.Models;

namespace Microservicio.Clientes.Business.Mappers
{
    public static class ClienteBusinessMapper
    {
        // 🔄 CrearClienteRequest → DataModel
        public static ClienteDataModel ToDataModel(CrearClienteRequest request)
        {
            if (request == null) return null;

            return new ClienteDataModel
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Cedula = request.Cedula,
                Correo = request.Correo,
                Telefono = request.Telefono,
                Estado = request.Estado

            };
        }

        // 🔄 ActualizarClienteRequest → DataModel
        public static ClienteDataModel ToDataModel(ActualizarClienteRequest request)
        {
            if (request == null) return null;

            return new ClienteDataModel
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Cedula = request.Cedula,
                Correo = request.Correo,
                Telefono = request.Telefono,
                Estado = request.Estado
            };
        }

        // 🔄 DataModel → ClienteResponse 💣
        public static ClienteResponse ToResponse(ClienteDataModel model)
        {
            if (model == null) return null;

            return new ClienteResponse
            {
                IdCliente = model.IdCliente,
                NombreCompleto = $"{model.Nombre} {model.Apellido}",
                Cedula = model.Cedula,
                Correo = model.Correo,
                Telefono = model.Telefono,
                Estado = model.Estado
            };
        }

        // 🔄 LISTA DataModel → LISTA Response 🔥
        public static IEnumerable<ClienteResponse> ToResponseList(IEnumerable<ClienteDataModel> models)
        {
            return models.Select(ToResponse);
        }
    }
}