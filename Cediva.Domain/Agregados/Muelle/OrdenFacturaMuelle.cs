using System;
using System.Collections.Generic;
using System.Linq;
using Cediva.Domain.Constantes;
using Cediva.Domain.Excepciones;
using Cediva.Dominio.Interfaces;

namespace Cediva.Domain.Agregados.Muelle
{
    public class OrdenFacturaMuelle : IAggregateRoot
    {
        // Ciclo de vida del flujo logístico referenciado a EstadosMuelle
        public const string EstadoPorAlistar = EstadosMuelle.PorAlistar;
        public const string EstadoAlistada = EstadosMuelle.Alistada;
        public const string EstadoAuditada = EstadosMuelle.Auditada;
        public const string EstadoDespachada = EstadosMuelle.Despachada;

        // Compatibilidad hacia atrás (legacy)
        public const string EstadoPendiente = EstadosMuelle.PendienteLegacy;
        public const string EstadoAuditadaLegacy = EstadosMuelle.AuditadaLegacy;

        public Guid Id { get; private set; }
        public string NumeroFactura { get; private set; } = string.Empty;
        public string ClienteNombre { get; private set; } = string.Empty;
        public string ClienteNit { get; private set; } = string.Empty;
        public string Muelle { get; private set; } = string.Empty;
        public string Estado { get; private set; } = EstadoPorAlistar;
        public DateTime FechaCreacion { get; private set; }

        // Datos de Alistamiento (Picking en bodega antes de llegar a muelle)
        public string? AlistadoPor { get; private set; }
        public string? HoraAlistamiento { get; private set; }

        // Datos de Auditoría (Packing y verificación física en muelle)
        public string? AuditadoPor { get; private set; }
        public string? HoraAuditoria { get; private set; }

        // Datos de Despacho (Cargue a vehículo de transporte)
        public string? DespachadoPor { get; private set; }
        public string? HoraDespacho { get; private set; }
        public string? Transportadora { get; private set; }
        public string? PlacaVehiculo { get; private set; }
        public string? Conductor { get; private set; }

        private readonly List<LineaOrdenFacturaMuelle> _lineas = new();
        public IReadOnlyCollection<LineaOrdenFacturaMuelle> Lineas => _lineas.AsReadOnly();

        private OrdenFacturaMuelle() { } // Requerido por EF Core

        public OrdenFacturaMuelle(
            Guid id,
            string numeroFactura,
            string clienteNombre,
            string clienteNit,
            string muelle)
        {
            if (string.IsNullOrWhiteSpace(numeroFactura))
                throw new ReglaDeNegocioException("El número de factura es obligatorio.");

            if (string.IsNullOrWhiteSpace(clienteNombre))
                throw new ReglaDeNegocioException("El nombre del cliente es obligatorio.");

            if (string.IsNullOrWhiteSpace(clienteNit))
                throw new ReglaDeNegocioException("El NIT del cliente es obligatorio.");

            if (string.IsNullOrWhiteSpace(muelle))
                throw new ReglaDeNegocioException("El muelle asignado es obligatorio.");

            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            NumeroFactura = numeroFactura.Trim();
            ClienteNombre = clienteNombre.Trim();
            ClienteNit = clienteNit.Trim();
            Muelle = muelle.Trim();
            Estado = EstadoPorAlistar;
            FechaCreacion = DateTime.UtcNow;
        }

        public void AgregarLinea(string codigoBarras, int cantidadPedida)
        {
            if (Estado != EstadoPorAlistar && Estado != EstadoPendiente)
                throw new ReglaDeNegocioException($"No se pueden agregar líneas a una orden que no esté en estado inicial '{EstadoPorAlistar}'. Estado actual: '{Estado}'.");

            var linea = new LineaOrdenFacturaMuelle(Guid.NewGuid(), Id, codigoBarras, cantidadPedida);
            _lineas.Add(linea);
        }

        public void Alistar(string alistadoPor, string? horaAlistamiento, IEnumerable<(string CodigoBarras, int CantidadAlistada)>? lineasAlistadas = null)
        {
            if (Estado != EstadoPorAlistar && Estado != EstadoPendiente)
                throw new ReglaDeNegocioException($"No se puede alistar la orden porque se encuentra en estado '{Estado}'. Solo se pueden alistar órdenes en estado '{EstadoPorAlistar}'.");

            if (string.IsNullOrWhiteSpace(alistadoPor))
                throw new ReglaDeNegocioException("El nombre de quien alista es obligatorio.");

            if (lineasAlistadas != null && lineasAlistadas.Any())
            {
                var alistadoDict = lineasAlistadas.ToDictionary(l => l.CodigoBarras, l => l.CantidadAlistada);
                foreach (var linea in _lineas)
                {
                    if (alistadoDict.TryGetValue(linea.CodigoBarras, out var alistado))
                    {
                        linea.ActualizarAlistado(alistado);
                    }
                }
            }
            else
            {
                // Si no se especifica desglose por línea, se marca el alistamiento completo según la cantidad pedida
                foreach (var linea in _lineas)
                {
                    linea.ActualizarAlistado(linea.CantidadPedida);
                }
            }

            AlistadoPor = alistadoPor.Trim();
            HoraAlistamiento = string.IsNullOrWhiteSpace(horaAlistamiento) ? DateTime.Now.ToString(FormatosLogistica.HoraEstandar) : horaAlistamiento.Trim();
            Estado = EstadoAlistada;
        }

        public void Auditar(string auditadoPor, string horaAuditoria, IEnumerable<(string CodigoBarras, int CantidadPistoleada)> lineasPistoleadas)
        {
            if (Estado != EstadoAlistada && Estado != EstadoPendiente)
                throw new ReglaDeNegocioException($"No se puede auditar la orden porque se encuentra en estado '{Estado}'. La orden debe estar previamente en estado '{EstadoAlistada}'.");

            if (string.IsNullOrWhiteSpace(auditadoPor))
                throw new ReglaDeNegocioException("El nombre del auditor es requerido.");

            var pistoleoDict = lineasPistoleadas.ToDictionary(l => l.CodigoBarras, l => l.CantidadPistoleada);

            foreach (var linea in _lineas)
            {
                if (pistoleoDict.TryGetValue(linea.CodigoBarras, out var pistoleado))
                {
                    linea.ActualizarPistoleado(pistoleado);
                }
            }

            AuditadoPor = auditadoPor.Trim();
            HoraAuditoria = string.IsNullOrWhiteSpace(horaAuditoria) ? DateTime.Now.ToString(FormatosLogistica.HoraEstandar) : horaAuditoria.Trim();
            Estado = EstadoAuditada;
        }

        public void Despachar(
            string despachadoPor,
            string horaDespacho,
            string transportadora,
            string placaVehiculo,
            string conductor)
        {
            if (Estado != EstadoAuditada && Estado != EstadoAuditadaLegacy)
                throw new ReglaDeNegocioException($"No se puede despachar la orden porque su estado actual es '{Estado}'. Debe estar en estado '{EstadoAuditada}'.");

            if (string.IsNullOrWhiteSpace(despachadoPor))
                throw new ReglaDeNegocioException("El nombre de quien despacha es obligatorio.");

            if (string.IsNullOrWhiteSpace(transportadora))
                throw new ReglaDeNegocioException("La empresa transportadora es obligatoria.");

            if (string.IsNullOrWhiteSpace(placaVehiculo))
                throw new ReglaDeNegocioException("La placa del vehículo es obligatoria.");

            if (string.IsNullOrWhiteSpace(conductor))
                throw new ReglaDeNegocioException("El nombre del conductor es obligatorio.");

            DespachadoPor = despachadoPor.Trim();
            HoraDespacho = string.IsNullOrWhiteSpace(horaDespacho) ? DateTime.Now.ToString(FormatosLogistica.HoraEstandar) : horaDespacho.Trim();
            Transportadora = transportadora.Trim();
            PlacaVehiculo = placaVehiculo.Trim().ToUpperInvariant();
            Conductor = conductor.Trim();
            Estado = EstadoDespachada;
        }
    }
}
