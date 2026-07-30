using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cediva.Dominio.Interfaces;

namespace Cediva.Domain.Agregados.Inventario
{
 public class Inventario : IAggregateRoot
    {
        public Guid Id { get; private set; }
    }
}
