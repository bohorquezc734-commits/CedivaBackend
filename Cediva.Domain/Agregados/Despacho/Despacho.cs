using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cediva.Dominio.Interfaces;

namespace Cediva.Domain.Agregados.Despacho
{
  public class Despacho : IAggregateRoot
    {
        public Guid Id { get; private set; }
    }
}
