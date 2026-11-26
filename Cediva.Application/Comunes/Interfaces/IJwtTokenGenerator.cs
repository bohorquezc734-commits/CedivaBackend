using System;
using Cediva.Domain.Agregados.Usuario;

namespace Cediva.Application.Comunes.Interfaces
{
    public interface IJwtTokenGenerator
    {
        (string Token, DateTime ExpiraEn) GenerarToken(Usuario usuario);
    }
}
