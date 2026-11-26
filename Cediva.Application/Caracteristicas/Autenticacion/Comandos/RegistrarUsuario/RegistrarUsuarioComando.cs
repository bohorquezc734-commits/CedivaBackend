using System;
using System.Threading;
using System.Threading.Tasks;
using Cediva.Application.Comunes.Interfaces;
using Cediva.Domain.Agregados.Usuario;
using Cediva.Domain.Enumeraciones;
using Cediva.Domain.Excepciones;
using Cediva.Domain.Interfaces;
using MediatR;

namespace Cediva.Application.Caracteristicas.Autenticacion.Comandos.RegistrarUsuario
{
    public record RegistrarUsuarioComando(
        string NombreCompleto,
        string Email,
        string Password,
        RolUsuario Rol
    ) : IRequest<Guid>;

    public class RegistrarUsuarioHandler : IRequestHandler<RegistrarUsuarioComando, Guid>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegistrarUsuarioHandler(IUsuarioRepository usuarioRepository, IPasswordHasher passwordHasher)
        {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<Guid> Handle(RegistrarUsuarioComando request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 6)
                throw new ReglaDeNegocioException("La contraseña debe tener al menos 6 caracteres.");

            var existe = await _usuarioRepository.ExisteEmail(request.Email, cancellationToken);
            if (existe)
                throw new ReglaDeNegocioException($"Ya existe un usuario registrado con el correo '{request.Email}'.");

            var passwordHash = _passwordHasher.Hash(request.Password);
            var usuario = new Usuario(
                Guid.NewGuid(),
                request.NombreCompleto,
                request.Email,
                passwordHash,
                request.Rol
            );

            await _usuarioRepository.Agregar(usuario, cancellationToken);
            await _usuarioRepository.GuardarCambios(usuario, cancellationToken);

            return usuario.Id;
        }
    }
}
