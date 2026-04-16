using Microservicio.Clientes.Api.Extensions;
using Microservicio.Clientes.Api.Middleware;
using Microservicio.Clientes.Api.Settings;
using Microservicio.Clientes.Business.Interfaces;
using Microservicio.Clientes.Business.Services;
using Microservicio.Clientes.DataManagement.Interfaces;
using Microservicio.Clientes.Api.Extensions;
using Microservicio.Clientes.Api.Middleware;
using Microservicio.Clientes.Api.Settings;

using Microservicio.Clientes.Business.Interfaces;
using Microservicio.Clientes.Business.Services;

using Microservicio.Clientes.DataManagement.Interfaces;
var builder = WebApplication.CreateBuilder(args);

// 🔥 CONFIGURACIÓN

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddSwaggerDocumentation();

// CORS
builder.Services.AddCustomCors(builder.Configuration);

// Versioning
builder.Services.AddApiVersioningConfig();

// JWT Settings
var jwtSettings = builder.Configuration
    .GetSection("JwtSettings")
    .Get<JwtSettings>();

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

// Authentication JWT
builder.Services.AddJwtAuthentication(builder.Configuration);

// Dependency Injection (DbContext, Services, Repositories)
builder.Services.AddApplicationServices(builder.Configuration);

// 🔥 AUTH SERVICE CON JWT REAL (IMPORTANTE 💣)
builder.Services.AddScoped<IAuthService>(sp =>
    new AuthService(
        sp.GetRequiredService<IUnitOfWork>(),
        jwtSettings.SecretKey,
        jwtSettings.Issuer,
        jwtSettings.Audience,
        jwtSettings.ExpirationMinutes
    )
);

var app = builder.Build();


// 🔥 MIDDLEWARE PIPELINE (ORDEN IMPORTANTE 💣)

// Manejo global de errores
app.UseMiddleware<ExceptionHandlingMiddleware>();

// HTTPS
app.UseHttpsRedirection();

// CORS
app.UseCors("CorsPolicy");

// Authentication + Authorization
app.UseAuthentication();
app.UseAuthorization();

// Swagger (solo en desarrollo)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Controllers
app.MapControllers();

app.Run();