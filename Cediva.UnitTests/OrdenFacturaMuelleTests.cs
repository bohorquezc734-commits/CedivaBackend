using System;
using System.Collections.Generic;
using Cediva.Domain.Agregados.Muelle;
using Cediva.Domain.Excepciones;
using Xunit;

namespace Cediva.UnitTests
{
    public class OrdenFacturaMuelleTests
    {
        [Fact]
        public void CrearOrdenMuelle_DebeIniciarEnEstadoPorAlistar_YLineasConCantidadesEnCero()
        {
            // Arrange & Act
            var orden = new OrdenFacturaMuelle(
                Guid.NewGuid(),
                "FAC-2026-0001",
                "Ferretería El Tornillo",
                "900123456-1",
                "Muelle 01"
            );
            orden.AgregarLinea("7701234567890", 10);

            // Assert
            Assert.Equal(OrdenFacturaMuelle.EstadoPorAlistar, orden.Estado);
            Assert.Null(orden.AlistadoPor);
            Assert.Null(orden.HoraAlistamiento);
            Assert.Null(orden.AuditadoPor);
            Assert.Null(orden.DespachadoPor);

            Assert.Single(orden.Lineas);
            var linea = Assert.Single(orden.Lineas);
            Assert.Equal(10, linea.CantidadPedida);
            Assert.Equal(0, linea.CantidadAlistada);
            Assert.Equal(0, linea.CantidadPistoleada);
        }

        [Fact]
        public void AlistarOrden_DesdePorAlistar_DebeCambiarEstadoAAlistada_YRegistrarAlistadorYHora()
        {
            // Arrange
            var orden = new OrdenFacturaMuelle(
                Guid.NewGuid(),
                "FAC-2026-0002",
                "Distribuidora Los Andes",
                "800987654-2",
                "Muelle 02"
            );
            orden.AgregarLinea("7701111111111", 5);
            orden.AgregarLinea("7702222222222", 8);

            var lineasAlistadas = new List<(string CodigoBarras, int CantidadAlistada)>
            {
                ("7701111111111", 5),
                ("7702222222222", 8)
            };

            // Act
            orden.Alistar("Juan Operario Picking", "08:30:00", lineasAlistadas);

            // Assert
            Assert.Equal(OrdenFacturaMuelle.EstadoAlistada, orden.Estado);
            Assert.Equal("Juan Operario Picking", orden.AlistadoPor);
            Assert.Equal("08:30:00", orden.HoraAlistamiento);

            var lineas = new List<LineaOrdenFacturaMuelle>(orden.Lineas);
            Assert.Equal(5, lineas[0].CantidadAlistada);
            Assert.Equal(0, lineas[0].CantidadPistoleada); // No debe sobreescribir la auditoría
            Assert.Equal(8, lineas[1].CantidadAlistada);
            Assert.Equal(0, lineas[1].CantidadPistoleada);
        }

        [Fact]
        public void AlistarOrden_SinDesgloseDeLineas_DebeMarcarAlistamientoCompleto()
        {
            // Arrange
            var orden = new OrdenFacturaMuelle(
                Guid.NewGuid(),
                "FAC-2026-0003",
                "Comercializadora Alfa",
                "901234567-3",
                "Muelle 03"
            );
            orden.AgregarLinea("7703333333333", 12);

            // Act
            orden.Alistar("Pedro Alistador", "09:00:00", null);

            // Assert
            Assert.Equal(OrdenFacturaMuelle.EstadoAlistada, orden.Estado);
            Assert.Equal(12, Assert.Single(orden.Lineas).CantidadAlistada);
        }

        [Fact]
        public void AuditarOrden_CuandoEstaEnPorAlistar_DebeLanzarReglaDeNegocioException()
        {
            // Arrange
            var orden = new OrdenFacturaMuelle(
                Guid.NewGuid(),
                "FAC-2026-0004",
                "Construcciones Beta",
                "901345678-4",
                "Muelle 04"
            );
            orden.AgregarLinea("7704444444444", 20);

            var lineasPistoleadas = new List<(string CodigoBarras, int CantidadPistoleada)>
            {
                ("7704444444444", 20)
            };

            // Act & Assert (Intentar auditar directamente sin haber alistado)
            var ex = Assert.Throws<ReglaDeNegocioException>(() =>
                orden.Auditar("Carlos Auditor Muelle", "10:00:00", lineasPistoleadas)
            );

            Assert.Contains("La orden debe estar previamente en estado 'alistada'", ex.Message);
        }

        [Fact]
        public void AuditarOrden_DesdeAlistada_DebeCambiarEstadoAAuditada_YRegistrarAuditor()
        {
            // Arrange
            var orden = new OrdenFacturaMuelle(
                Guid.NewGuid(),
                "FAC-2026-0005",
                "Supermercados Delta",
                "901456789-5",
                "Muelle 05"
            );
            orden.AgregarLinea("7705555555555", 15);
            orden.Alistar("Juan Alistador", "08:15:00", null);

            var lineasPistoleadas = new List<(string CodigoBarras, int CantidadPistoleada)>
            {
                ("7705555555555", 15)
            };

            // Act
            orden.Auditar("Ana Auditora Muelle", "09:45:00", lineasPistoleadas);

            // Assert
            Assert.Equal(OrdenFacturaMuelle.EstadoAuditada, orden.Estado);
            Assert.Equal("Ana Auditora Muelle", orden.AuditadoPor);
            Assert.Equal("09:45:00", orden.HoraAuditoria);

            var linea = Assert.Single(orden.Lineas);
            Assert.Equal(15, linea.CantidadAlistada);
            Assert.Equal(15, linea.CantidadPistoleada);
        }

        [Theory]
        [InlineData(OrdenFacturaMuelle.EstadoPorAlistar)]
        [InlineData(OrdenFacturaMuelle.EstadoAlistada)]
        public void DespacharOrden_CuandoNoEstaAuditada_DebeLanzarReglaDeNegocioException(string estadoPrevio)
        {
            // Arrange
            var orden = new OrdenFacturaMuelle(
                Guid.NewGuid(),
                "FAC-2026-0006",
                "Almacenes Gamma",
                "901567890-6",
                "Muelle 06"
            );
            orden.AgregarLinea("7706666666666", 6);

            if (estadoPrevio == OrdenFacturaMuelle.EstadoAlistada)
            {
                orden.Alistar("Juan Alistador", "10:00:00", null);
            }

            // Act & Assert
            var ex = Assert.Throws<ReglaDeNegocioException>(() =>
                orden.Despachar(
                    "Despachador Bodega",
                    "11:00:00",
                    "Envía",
                    "WXY-789",
                    "Carlos Conductor"
                )
            );

            Assert.Contains("Debe estar en estado 'auditada'", ex.Message);
        }

        [Fact]
        public void DespacharOrden_DesdeAuditada_DebeCambiarEstadoADespachada_YRegistrarTransporte()
        {
            // Arrange
            var orden = new OrdenFacturaMuelle(
                Guid.NewGuid(),
                "FAC-2026-0007",
                "Industrias Omega",
                "901678901-7",
                "Muelle 07"
            );
            orden.AgregarLinea("7707777777777", 30);
            orden.Alistar("Operario Picking", "07:00:00", null);
            orden.Auditar("Auditor Muelle", "08:00:00", new List<(string, int)> { ("7707777777777", 30) });

            // Act
            orden.Despachar(
                "Supervisor Despachos",
                "09:15:00",
                "TCC Logística",
                "abc-123",
                "Manuel Gómez"
            );

            // Assert
            Assert.Equal(OrdenFacturaMuelle.EstadoDespachada, orden.Estado);
            Assert.Equal("Supervisor Despachos", orden.DespachadoPor);
            Assert.Equal("09:15:00", orden.HoraDespacho);
            Assert.Equal("TCC Logística", orden.Transportadora);
            Assert.Equal("ABC-123", orden.PlacaVehiculo); // Formato uppercase
            Assert.Equal("Manuel Gómez", orden.Conductor);
        }

        [Fact]
        public void ActualizarAlistado_ConCantidadNegativa_DebeLanzarReglaDeNegocioException()
        {
            // Arrange
            var linea = new LineaOrdenFacturaMuelle(Guid.NewGuid(), Guid.NewGuid(), "7709999999999", 10);

            // Act & Assert
            var ex = Assert.Throws<ReglaDeNegocioException>(() => linea.ActualizarAlistado(-1));
            Assert.Contains("La cantidad alistada no puede ser negativa", ex.Message);
        }

        [Fact]
        public void AgregarLinea_CuandoOrdenYaFueAlistada_DebeLanzarReglaDeNegocioException()
        {
            // Arrange
            var orden = new OrdenFacturaMuelle(
                Guid.NewGuid(),
                "FAC-2026-0008",
                "Cliente Test",
                "900000000-0",
                "Muelle 08"
            );
            orden.AgregarLinea("7708888888888", 5);
            orden.Alistar("Operario", "08:00:00", null);

            // Act & Assert
            var ex = Assert.Throws<ReglaDeNegocioException>(() =>
                orden.AgregarLinea("7709999999999", 3)
            );

            Assert.Contains("No se pueden agregar líneas a una orden que no esté en estado inicial", ex.Message);
        }
    }
}
