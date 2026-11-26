using System;
using System.Threading;
using System.Threading.Tasks;
using Cediva.Application.Caracteristicas.Autenticacion.DTOs;
using Cediva.Application.Comunes.Interfaces;
using Cediva.Domain.Excepciones;
using Cediva.Domain.Interfaces;
using MediatR;

namespace Cediva.Application.Caracteristicas.Autenticacion.Comandos.Login
{
    public record LoginComando(string Email, string Password) : IRequest<LoginResponseDto>;

    public class LoginHandler : IRequestHandler<LoginComando, LoginResponseDto>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;

        public LoginHandler(
            IUsuarioRepository usuarioRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenGenerator jwtTokenGenerator)
        {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponseDto> Handle(LoginComando request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
                throw new ReglaDeNegocioException("El correo y la contraseña son requeridos.");

            var usuario = await _usuarioRepository.ObtenerPorEmail(request.Email, cancellationToken);
            if (usuario == null)
                throw new ReglaDeNegocioException("Credenciales inválidas.");

            if (!usuario.Activo)
                throw new ReglaDeNegocioException("El usuario se encuentra inactivo. Comuníquese con el administrador.");

            var esPasswordValido = _passwordHasher.Verificar(request.Password, usuario.PasswordHash);
            if (!esPasswordValido)
                throw new ReglaDeNegocioException("Credenciales inválidas.");

            var (token, expiraEn) = _jwtTokenGenerator.GenerarToken(usuario);

            var usuarioDto = new UsuarioInfoDto(
                usuario.Id,
                usuario.NombreCompleto,
                usuario.Email,
                usuario.Rol.ToString()
            );

            return new LoginResponseDto(token, expiraEn, usuarioDto);
        }
    }
}
