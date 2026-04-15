namespace Microservicio.Clientes.Api.Models
{
    public class ApiErrorResponse
    {
        public bool Success { get; set; } = false;

        public string Message { get; set; }

        public IEnumerable<string>? Errors { get; set; }
    }
}
