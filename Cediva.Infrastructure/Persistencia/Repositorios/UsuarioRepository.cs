using System.Threading;
using System.Threading.Tasks;
using Cediva.Domain.Agregados.Usuario;
using Cediva.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cediva.Infrastructure.Persistencia.Repositorios
{
    public class UsuarioRepository : RepositorioBase<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Usuario?> ObtenerPorEmail(string email, CancellationToken cancellationToken = default)
        {
            var emailNormalizado = email.Trim().ToLower();
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email.ToLower() == emailNormalizado, cancellationToken);
        }

        public async Task<bool> ExisteEmail(string email, CancellationToken cancellationToken = default)
        {
            var emailNormalizado = email.Trim().ToLower();
            return await _context.Usuarios
                .AnyAsync(u => u.Email.ToLower() == emailNormalizado, cancellationToken);
        }
    }
}
