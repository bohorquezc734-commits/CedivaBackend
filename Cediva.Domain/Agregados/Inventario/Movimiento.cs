using Cediva.Domain.Enumeraciones;
using Cediva.Dominio.ObjetosValor;

namespace Cediva.Domain.Agregados.Inventario
{
    public class Movimiento
    {
        public Guid Id { get; private set; }
        public TipoMovimiento TipoMovimiento { get; private set; }
        public Cantidad Cantidad { get; private set; }
        public Guid InventarioId { get; private set; }
    }
}
