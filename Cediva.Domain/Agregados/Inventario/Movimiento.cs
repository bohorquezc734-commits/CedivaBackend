using Cediva.Domain.Enumeraciones;
using Cediva.Dominio.ObjetosValor;

namespace Cediva.Domain.Agregados.Inventario
{
    public class Movimiento
    {
        public Guid Id { get; private set; }
        public TipoMovimiento Tipo { get; private set; }
        public Cantidad Cantidad { get; private set; }
        public Guid InventarioId { get; private set; }
        public DateTime FechaMovimiento { get; private set; }

        private Movimiento() { } 

        public Movimiento(Guid id, TipoMovimiento tipo, Cantidad cantidad, Guid inventarioId)
        {
            Id = id;
            Tipo = tipo;
            Cantidad = cantidad;
            InventarioId = inventarioId;
            FechaMovimiento = DateTime.Now;
        }
    }
}