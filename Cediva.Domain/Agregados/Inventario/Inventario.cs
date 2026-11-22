using System;
using System.Collections.Generic;
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

        
        private Inventario() { }

        public Inventario(Guid id, Guid productoId, Cantidad cantidadInicial)
        {
            Id = id;
            ProductoId = productoId;
            CantidadDisponible = cantidadInicial;
        }

        public void ActualizarCantidad(Cantidad nuevaCantidad)
        {
            CantidadDisponible = nuevaCantidad;
        }

        public void AgregarMovimiento(Movimiento movimiento)
        {
            _movimientos.Add(movimiento);
        }
    }
}