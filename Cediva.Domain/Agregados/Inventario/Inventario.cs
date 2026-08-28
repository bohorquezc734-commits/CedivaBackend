using Cediva.Domain.Enumeraciones;
using Cediva.Dominio.Interfaces;
using Cediva.Dominio.ObjetosValor;

namespace Cediva.Domain.Agregados.Inventario
{
    public class Inventario : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public Guid ProductoId { get; private set; }
        public Cantidad CantidadDisponible { get; private set; }
        
        private readonly List<Movimiento> _movimientos = new();
        public IReadOnlyCollection<Movimiento> Movimientos => _movimientos.AsReadOnly();
    }
}
