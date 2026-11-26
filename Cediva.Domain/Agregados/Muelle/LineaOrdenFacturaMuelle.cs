using System;
using Cediva.Domain.Excepciones;

namespace Cediva.Domain.Agregados.Muelle
{
    public class LineaOrdenFacturaMuelle
    {
        public Guid Id { get; private set; }
        public Guid OrdenFacturaMuelleId { get; private set; }
        public string CodigoBarras { get; private set; } = string.Empty;
        public int CantidadPedida { get; private set; }
        public int CantidadPistoleada { get; private set; }

        private LineaOrdenFacturaMuelle() { } // EF Core

        public LineaOrdenFacturaMuelle(Guid id, Guid ordenFacturaMuelleId, string codigoBarras, int cantidadPedida)
        {
            if (string.IsNullOrWhiteSpace(codigoBarras))
                throw new ReglaDeNegocioException("El código de barras de la línea no puede estar vacío.");

            if (cantidadPedida <= 0)
                throw new ReglaDeNegocioException("La cantidad pedida debe ser mayor a cero.");

            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            OrdenFacturaMuelleId = ordenFacturaMuelleId;
            CodigoBarras = codigoBarras.Trim();
            CantidadPedida = cantidadPedida;
            CantidadPistoleada = 0;
        }

        public void ActualizarPistoleado(int cantidadPistoleada)
        {
            if (cantidadPistoleada < 0)
                throw new ReglaDeNegocioException("La cantidad pistoleada no puede ser negativa.");

            CantidadPistoleada = cantidadPistoleada;
        }
    }
}
