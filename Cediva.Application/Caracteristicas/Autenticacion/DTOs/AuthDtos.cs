using System;

namespace Cediva.Application.Caracteristicas.Autenticacion.DTOs
{
    public record UsuarioInfoDto(Guid Id, string NombreCompleto, string Email, string Rol);

    public record LoginResponseDto(string Token, DateTime ExpiraEn, UsuarioInfoDto Usuario);
}
