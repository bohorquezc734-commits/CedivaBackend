using Cediva.Domain.Agregados.Cliente;
using Cediva.Dominio.Interfaces;

namespace Cediva.Infrastructure.Persistencia.Repositorios
{
    public class RepositorioCliente : RepositorioBase<Cliente>, IRepositorioCliente
    {
        public RepositorioCliente(ApplicationDbContext context) : base(context) { }
    }
}