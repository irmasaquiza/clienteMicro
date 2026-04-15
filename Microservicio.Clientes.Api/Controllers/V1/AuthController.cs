using Asp.Versioning;
using Microservicio.Clientes.Api.Models;
using Microservicio.Clientes.Business.DTOs;
using Microservicio.Clientes.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Microservicio.Clientes.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // 🔐 POST: api/v1/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);

            var response = new ApiResponse<LoginResponse>
            {
                Message = "Login exitoso",
                Data = result
            };

            return Ok(response);
        }
    }
}