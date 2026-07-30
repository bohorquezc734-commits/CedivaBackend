
using Cediva.Dominio.Excepciones;
using Cediva.Dominio.Interfaces;

namespace Cediva.Dominio.Agregados.Producto;

public class Marca : IAggregateRoot
{
    public Guid Id { get; private set; }
    public string Nombre { get; private set; }
    public string? Descripcion { get; private set; }
    public bool EstaActivo { get; private set; }
    public DateTime FechaCreacion { get; private set; }

    private Marca() { } // Para EF Core

    public Marca(string nombre, string? descripcion = null)
    {
        Id = Guid.NewGuid();
        SetNombre(nombre);
        Descripcion = descripcion;
        EstaActivo = true;
        FechaCreacion = DateTime.UtcNow;
    }

    public void Actualizar(string nombre, string? descripcion = null)
    {
        SetNombre(nombre);
        Descripcion = descripcion;
    }

    public void Activar() => EstaActivo = true;
    public void Desactivar() => EstaActivo = false;

    private void SetNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ExcepcionDominio("El nombre de la marca no puede estar vacío");

        if (nombre.Length < 2)
            throw new ExcepcionDominio("El nombre de la marca debe tener al menos 2 caracteres");

        if (nombre.Length > 50)
            throw new ExcepcionDominio("El nombre de la marca no puede tener más de 50 caracteres");

        Nombre = nombre.Trim();
    }
}