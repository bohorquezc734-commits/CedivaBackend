using System;
using Cediva.Domain.Agregados.Usuario;
using Cediva.Domain.Enumeraciones;
using Xunit;

namespace Cediva.UnitTests
{
    public class UsuarioRolesTests
    {
        [Fact]
        public void RolUsuario_ValoresNumericos_DebenMantenerseEstables()
        {
            // Assert
            Assert.Equal(1, (int)RolUsuario.Administrador);
            Assert.Equal(2, (int)RolUsuario.Supervisor);
            Assert.Equal(3, (int)RolUsuario.AuditorBodega);
            Assert.Equal(4, (int)RolUsuario.AuxiliarLogistico);
        }

        [Fact]
        public void RolUsuario_AuxiliarLogistico_DebeRepresentarseEnTextoCorrectamente()
        {
            // Assert
            Assert.Equal("AuxiliarLogistico", RolUsuario.AuxiliarLogistico.ToString());
        }

        [Fact]
        public void CrearUsuario_ConRolAuxiliarLogistico_DebeCrearseCorrectamente()
        {
            // Arrange & Act
            var usuario = new Usuario(
                Guid.NewGuid(),
                "Pedro Auxiliar Picking",
                "pedro.picking@cediva.com",
                "hashedPassword123",
                RolUsuario.AuxiliarLogistico
            );

            // Assert
            Assert.Equal("Pedro Auxiliar Picking", usuario.NombreCompleto);
            Assert.Equal("pedro.picking@cediva.com", usuario.Email);
            Assert.Equal(RolUsuario.AuxiliarLogistico, usuario.Rol);
            Assert.True(usuario.Activo);
        }

        [Fact]
        public void ActualizarDatos_CambiandoARolAuxiliarLogistico_DebeActualizarRolCorrectamente()
        {
            // Arrange
            var usuario = new Usuario(
                Guid.NewGuid(),
                "Operador Temporal",
                "operador@cediva.com",
                "hashedPassword123",
                RolUsuario.AuditorBodega
            );

            // Act
            usuario.ActualizarDatos("Operador Oficial Picking", RolUsuario.AuxiliarLogistico);

            // Assert
            Assert.Equal("Operador Oficial Picking", usuario.NombreCompleto);
            Assert.Equal(RolUsuario.AuxiliarLogistico, usuario.Rol);
        }
    }
}
