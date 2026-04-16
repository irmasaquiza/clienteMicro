using Asp.Versioning;
using Microservicio.Clientes.Api.Models;
using Microservicio.Clientes.Business.DTOs;
using Microservicio.Clientes.Business.Interfaces;
using Microservicio.Clientes.DataManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microservicio.Clientes.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/clientes")]
    [Authorize] // 🔥 AQUÍ

    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // 🔍 GET: api/v1/clientes
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _clienteService.GetAllAsync();

            return Ok(new ApiResponse<IEnumerable<ClienteResponse>>
            {
                Message = "Consulta exitosa",
                Data = result
            });
        }

        // 🔍 GET: api/v1/clientes/{id}
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _clienteService.GetByIdAsync(id);

            return Ok(new ApiResponse<ClienteResponse>
            {
                Message = "Consulta exitosa",
                Data = result
            });
        }

        // 🔍 GET: api/v1/clientes/cedula/{cedula}
        [HttpGet("cedula/{cedula}")]
        public async Task<IActionResult> GetByCedula(string cedula)
        {
            var result = await _clienteService.GetAllAsync();

            var cliente = result.FirstOrDefault(c => c.Cedula == cedula);

            return Ok(new ApiResponse<ClienteResponse>
            {
                Message = "Consulta exitosa",
                Data = cliente
            });
        }

        // 📄 POST: api/v1/clientes
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CrearClienteRequest request)
        {
            var result = await _clienteService.CreateAsync(request);

            return Ok(new ApiResponse<ClienteResponse>
            {
                Message = "Cliente creado correctamente",
                Data = result
            });
        }

        // ✏️ PUT: api/v1/clientes/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ActualizarClienteRequest request)
        {
            await _clienteService.UpdateAsync(id, request);

            return Ok(new ApiResponse<string>
            {
                Message = "Cliente actualizado correctamente",
                Data = "OK"
            });
        }

        // ❌ DELETE: api/v1/clientes/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _clienteService.DeleteAsync(id);

            return Ok(new ApiResponse<string>
            {
                Message = "Cliente eliminado correctamente",
                Data = "OK"
            });
        }

        // 🔥 GET PAGINADO
        [HttpPost("filter")]
        public async Task<IActionResult> GetPaged([FromBody] ClienteFiltroRequest request)
        {
            var result = await _clienteService.GetPagedAsync(request);

            return Ok(new ApiResponse<DataPagedResult<ClienteResponse>>
            {
                Message = "Consulta paginada exitosa",
                Data = result
            });
        }
    }
}