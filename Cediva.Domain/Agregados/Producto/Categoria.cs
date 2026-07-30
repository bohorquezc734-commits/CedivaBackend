
using Cediva.Dominio.Excepciones;
using Cediva.Dominio.Interfaces;

namespace Cediva.Dominio.Agregados.Producto;

public class Categoria : IAggregateRoot
{
    public Guid Id { get; private set; }
    public string Nombre { get; private set; }
    public string? Descripcion { get; private set; }
    public Categoria? CategoriaPadre { get; private set; }
    public bool EstaActivo { get; private set; }
    public DateTime FechaCreacion { get; private set; }

    private Categoria() { } // Para EF Core

    public Categoria(string nombre, string? descripcion = null, Categoria? categoriaPadre = null)
    {
        Id = Guid.NewGuid();
        SetNombre(nombre);
        Descripcion = descripcion;
        CategoriaPadre = categoriaPadre;
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
            throw new ExcepcionDominio("El nombre de la categoría no puede estar vacío");

        if (nombre.Length < 3)
            throw new ExcepcionDominio("El nombre de la categoría debe tener al menos 3 caracteres");

        if (nombre.Length > 50)
            throw new ExcepcionDominio("El nombre de la categoría no puede tener más de 50 caracteres");

        Nombre = nombre.Trim();
    }
}