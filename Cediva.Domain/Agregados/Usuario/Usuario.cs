using System;
using System.Text.RegularExpressions;
using Cediva.Domain.Constantes;
using Cediva.Domain.Enumeraciones;
using Cediva.Domain.Excepciones;
using Cediva.Dominio.Interfaces;

namespace Cediva.Domain.Agregados.Usuario
{
    public class Usuario : IAggregateRoot
    {
        public Guid Id { get; private set; }
        public string NombreCompleto { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public RolUsuario Rol { get; private set; }
        public bool Activo { get; private set; }
        public DateTime FechaCreacion { get; private set; }

        private Usuario()
        {
        }

        public Usuario(Guid id, string nombreCompleto, string email, string passwordHash, RolUsuario rol)
        {
            if (id == Guid.Empty)
                throw new ReglaDeNegocioException("El Id de usuario no es válido.");

            SetNombreCompleto(nombreCompleto);
            SetEmail(email);
            SetPasswordHash(passwordHash);

            Id = id;
            Rol = rol;
            Activo = true;
            FechaCreacion = DateTime.UtcNow;
        }

        public void Activar() => Activo = true;

        public void Desactivar() => Activo = false;

        public void CambiarPassword(string nuevoPasswordHash)
        {
            SetPasswordHash(nuevoPasswordHash);
        }

        public void ActualizarDatos(string nombreCompleto, RolUsuario nuevoRol)
        {
            SetNombreCompleto(nombreCompleto);
            Rol = nuevoRol;
        }

        private void SetNombreCompleto(string nombreCompleto)
        {
            if (string.IsNullOrWhiteSpace(nombreCompleto))
                throw new ReglaDeNegocioException("El nombre completo del usuario es obligatorio.");

            if (nombreCompleto.Trim().Length < LimitesDominio.Usuario.LongitudMinimaNombre)
                throw new ReglaDeNegocioException($"El nombre completo debe tener al menos {LimitesDominio.Usuario.LongitudMinimaNombre} caracteres.");

            NombreCompleto = nombreCompleto.Trim();
        }

        private void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ReglaDeNegocioException("El correo electrónico es obligatorio.");

            var emailLimpio = email.Trim().ToLowerInvariant();
            if (!Regex.IsMatch(emailLimpio, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ReglaDeNegocioException("El formato del correo electrónico no es válido.");

            Email = emailLimpio;
        }

        private void SetPasswordHash(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ReglaDeNegocioException("El hash de contraseña es obligatorio.");

            PasswordHash = passwordHash;
        }
    }
}
