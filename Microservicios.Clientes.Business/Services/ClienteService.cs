using System;
using System.Collections.Generic;
using System.Text;

using Microservicio.Clientes.Business.DTOs;
using Microservicio.Clientes.Business.Interfaces;
using Microservicio.Clientes.Business.Mappers;
using Microservicio.Clientes.Business.Validators;
using Microservicio.Clientes.Business.Exceptions;
using Microservicio.Clientes.DataManagement.Interfaces;
using Microservicio.Clientes.DataManagement.Models;

namespace Microservicio.Clientes.Business.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteDataService _clienteDataService;

        public ClienteService(IClienteDataService clienteDataService)
        {
            _clienteDataService = clienteDataService;
        }

        // 🔍 CONSULTAS

        public async Task<ClienteResponse?> GetByIdAsync(int id)
        {
            var cliente = await _clienteDataService.GetByIdAsync(id);

            if (cliente == null)
                throw new NotFoundException($"Cliente con ID {id} no encontrado");

            return ClienteBusinessMapper.ToResponse(cliente);
        }

        public async Task<IEnumerable<ClienteResponse>> GetAllAsync()
        {
            var clientes = await _clienteDataService.GetAllAsync();
            return ClienteBusinessMapper.ToResponseList(clientes);
        }

        public async Task<DataPagedResult<ClienteResponse>> GetPagedAsync(ClienteFiltroRequest request)
        {
            // 🔥 Mapear filtro DTO → DataModel
            var filtro = new ClienteFiltroDataModel
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Cedula = request.Cedula,
                Correo = request.Correo,
                Estado = request.Estado,
                Page = request.Page,
                PageSize = request.PageSize,
                OrderBy = request.OrderBy,
                Descending = request.Descending
            };

            var result = await _clienteDataService.GetPagedAsync(filtro);

            return new DataPagedResult<ClienteResponse>
            {
                Data = result.Data.Select(ClienteBusinessMapper.ToResponse),
                Total = result.Total,
                Page = result.Page,
                PageSize = result.PageSize
            };
        }

        // ✏️ OPERACIONES

        public async Task<ClienteResponse> CreateAsync(CrearClienteRequest request)
        {
            // 🔥 VALIDACIÓN
            ClienteValidator.ValidarCrear(request);
            var existe = await _clienteDataService.GetByCedulaAsync(request.Cedula);

            if (existe != null)
            {
                throw new ValidationException(new List<string>
        {
            "Ya existe un cliente con ese número de identificación"
        });
            }
            // 🔥 MAPEAR DTO → DATA MODEL
            var model = ClienteBusinessMapper.ToDataModel(request);
            model.UsuarioCreacion = "admin"; // 🔥 obligatorio
            model.FechaCreacion = DateTime.Now;
            model.Eliminado = false;

            // 🔥 GUARDAR
            var created = await _clienteDataService.CreateAsync(model);

            return ClienteBusinessMapper.ToResponse(created);
        }

        public async Task<bool> UpdateAsync(int id, ActualizarClienteRequest request)
        {
            // 🔥 VALIDACIÓN
            ClienteValidator.ValidarActualizar(request);

            // 🔥 MAPEAR DTO → DATA MODEL
            var model = ClienteBusinessMapper.ToDataModel(request);

            var updated = await _clienteDataService.UpdateAsync(id, model);

            if (!updated)
                throw new NotFoundException($"Cliente con ID {id} no encontrado");

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var deleted = await _clienteDataService.DeleteAsync(id);

            if (!deleted)
                throw new NotFoundException($"Cliente con ID {id} no encontrado");

            return true;
        }
    }
}