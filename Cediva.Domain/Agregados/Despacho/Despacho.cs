using Cediva.Domain.Enumeraciones;
using Cediva.Dominio.Interfaces;
using Cediva.Dominio.ObjetosValor;

namespace Cediva.Domain.Agregados.Despacho
{
    public class Despacho : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public EstadoDespacho Estado { get; private set; }
        public Direccion DireccionEntrega { get; private set; }
        
        private readonly List<ItemDespacho> _items = new();
        public IReadOnlyCollection<ItemDespacho> Items => _items.AsReadOnly();
    }
}
