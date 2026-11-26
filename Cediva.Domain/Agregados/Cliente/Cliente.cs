using System;
using Cediva.Dominio.Interfaces;

namespace Cediva.Domain.Agregados.Cliente
{
    public class Cliente : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string Nombre { get; private set; }
        public string NIT { get; private set; } 

        private Cliente()
        {
            Nombre = string.Empty;
            NIT = string.Empty;
        }

        public Cliente(Guid id, string nombre, string NIT)
        {
            Id = id;
            Nombre = nombre;
            this.NIT = NIT; 
        }
    }
}