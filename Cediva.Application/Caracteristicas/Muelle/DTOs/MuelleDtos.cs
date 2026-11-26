using System;
using System.Collections.Generic;

namespace Cediva.Application.Caracteristicas.Muelle.DTOs
{
    public record LineaOrdenMuelleDto(
        Guid Id,
        string CodigoBarras,
        int CantidadPedida,
        int CantidadPistoleada
    );

    public record OrdenFacturaMuelleDto(
        Guid Id,
        string NumeroFactura,
        string ClienteNombre,
        string ClienteNit,
        string Muelle,
        string Estado,
        DateTime FechaCreacion,
        string? AuditadoPor,
        string? HoraAuditoria,
        string? DespachadoPor,
        string? HoraDespacho,
        string? Transportadora,
        string? PlacaVehiculo,
        string? Conductor,
        List<LineaOrdenMuelleDto> Lineas
    );

    public record CrearLineaOrdenDto(
        string CodigoBarras,
        int CantidadPedida
    );

    public record AuditarLineaDto(
        string CodigoBarras,
        int CantidadPistoleada
    );
}
