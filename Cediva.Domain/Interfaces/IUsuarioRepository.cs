using System.Threading;
using System.Threading.Tasks;
using Cediva.Domain.Agregados.Usuario;
using Cediva.Dominio.Interfaces;

namespace Cediva.Domain.Interfaces
{
    public interface IUsuarioRepository : IRepositorio<Usuario>
    {
        Task<Usuario?> ObtenerPorEmail(string email, CancellationToken cancellationToken = default);
        Task<bool> ExisteEmail(string email, CancellationToken cancellationToken = default);
    }
}
