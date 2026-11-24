using System;
using Cediva.Dominio.ObjetosValor;

namespace Cediva.Domain.Agregados.Despacho
{
    public class ItemDespacho
    {
        public Guid Id { get; private set; }
        public Guid ProductoId { get; private set; }
        public Cantidad ?Cantidad { get; private set; } 

        private ItemDespacho() { }

        public ItemDespacho(Guid id, Guid productoId, Cantidad cantidad)
        {
            Id = id;
            ProductoId = productoId;
            Cantidad = cantidad;
        }
    }
}