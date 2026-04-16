using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

using Microservicio.Clientes.Business.DTOs;
using Microservicio.Clientes.Business.Interfaces;
using Microservicio.Clientes.Business.Exceptions;
using Microservicio.Clientes.DataManagement.Interfaces;

using Microsoft.IdentityModel.Tokens;

namespace Microservicio.Clientes.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;

        // 🔥 CONFIG JWT (INYECTADA DESDE API)
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expiration;

        public AuthService(
            IUnitOfWork unitOfWork,
            string secretKey,
            string issuer,
            string audience,
            int expiration)
        {
            _unitOfWork = unitOfWork;
            _secretKey = secretKey;
            _issuer = issuer;
            _audience = audience;
            _expiration = expiration;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            // 🔍 BUSCAR USUARIO CON ROLES
            var usuario = await _unitOfWork.Usuarios.GetWithRolesAsync(request.Username);

            if (usuario == null)
                throw new UnauthorizedBusinessException("Usuario o contraseña incorrectos");

            // 🔒 VALIDAR BLOQUEO
            if (usuario.Bloqueado)
                throw new UnauthorizedBusinessException("Usuario bloqueado");

            // 🔑 VALIDAR PASSWORD (SIN HASH 🔥)
            if (usuario.PasswordHash != request.Password)
            {
                await _unitOfWork.Usuarios.IncrementarIntentosFallidosAsync(usuario.IdUsuario);
                await _unitOfWork.SaveChangesAsync();

                throw new UnauthorizedBusinessException("Usuario o contraseña incorrectos");
            }

            // 🔄 RESET INTENTOS
            await _unitOfWork.Usuarios.ResetIntentosFallidosAsync(usuario.IdUsuario);

            // 🔥 EXTRAER ROLES
            var roles = usuario.UsuarioRoles
                .Select(ur => ur.Rol.Nombre)
                .ToList();

            // 🔐 GENERAR JWT REAL 💣

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 🔥 CLAIMS
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Username),
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString())
            };

            // 🔥 AGREGAR ROLES
            foreach (var rol in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol));
            }

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_expiration),
                signingCredentials: creds
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            await _unitOfWork.SaveChangesAsync();

            return new LoginResponse
            {
                Token = token,
                Username = usuario.Username,
                Roles = roles,
                Expiration = tokenDescriptor.ValidTo
            };
        }
    }
}