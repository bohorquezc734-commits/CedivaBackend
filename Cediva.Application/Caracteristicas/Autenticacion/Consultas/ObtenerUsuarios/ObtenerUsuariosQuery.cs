using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cediva.Application.Caracteristicas.Autenticacion.DTOs;
using Cediva.Domain.Interfaces;
using MediatR;

namespace Cediva.Application.Caracteristicas.Autenticacion.Consultas.ObtenerUsuarios
{
    public record ObtenerUsuariosQuery() : IRequest<List<UsuarioInfoDto>>;

    public class ObtenerUsuariosHandler : IRequestHandler<ObtenerUsuariosQuery, List<UsuarioInfoDto>>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public ObtenerUsuariosHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<UsuarioInfoDto>> Handle(ObtenerUsuariosQuery request, CancellationToken cancellationToken)
        {
            var usuarios = await _usuarioRepository.ObtenerTodos(cancellationToken);

            return usuarios.Select(u => new UsuarioInfoDto(
                u.Id,
                u.NombreCompleto,
                u.Email,
                u.Rol.ToString()
            )).ToList();
        }
    }
}
