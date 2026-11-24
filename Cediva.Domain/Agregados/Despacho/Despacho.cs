using System;
using System.Collections.Generic;
using Cediva.Dominio.Interfaces;
using Cediva.Domain.Enumeraciones;
using Cediva.Dominio.ObjetosValor;

namespace Cediva.Domain.Agregados.Despacho
{
    public class Despacho : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string ClienteNombre { get; private set; }
        public DateTime FechaDespacho { get; private set; }
        public EstadoDespacho Estado { get; private set; }
        public Direccion DireccionEntrega { get; private set; } 

        private readonly List<ItemDespacho> _items = new();
        public IReadOnlyCollection<ItemDespacho> Items => _items.AsReadOnly();

        private Despacho() { }

     
        public  Despacho(Guid id, string clienteNombre, DateTime fecha, EstadoDespacho estado, Direccion direccion)
        {
            Id = id;
            ClienteNombre = clienteNombre;
            FechaDespacho = fecha;
            Estado = estado;
            DireccionEntrega = direccion;
        }

        public void AgregarItem(ItemDespacho item)
        {
            _items.Add(item);
        }
    }
}