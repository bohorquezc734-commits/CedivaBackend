using Cediva.Domain.Enumeraciones;

using Cediva.Dominio.Excepciones;
using Cediva.Dominio.Interfaces;
using Cediva.Dominio.ObjetosValor;

namespace Cediva.Dominio.Agregados.Producto;

public class Producto : IAggregateRoot
{
    public Guid Id { get; private set; }
    public string Nombre { get; private set; }
    public string? Descripcion { get; private set; }
    public CodigoBarras CodigoBarras { get; private set; }
    public Categoria Categoria { get; private set; }
    public Marca? Marca { get; private set; }
    public decimal PrecioUnitario { get; private set; }
    public UnidadMedida UnidadMedida { get; private set; }
    public bool EstaActivo { get; private set; }
    public DateTime FechaCreacion { get; private set; }
    public DateTime? FechaActualizacion { get; private set; }
    public string? ImagenUrl { get; private set; }
    public int PuntoReorden { get; private set; }

    private Producto() { } // Para EF Core

    public Producto(
        string nombre,
        string? descripcion,
        CodigoBarras codigoBarras,
        Categoria categoria,
        decimal precioUnitario,
        UnidadMedida unidadMedida,
        Marca? marca = null,
        string? imagenUrl = null,
        int puntoReorden = 10)
    {
        Id = Guid.NewGuid();
        SetNombre(nombre);
        Descripcion = descripcion;
        CodigoBarras = codigoBarras ?? throw new ExcepcionDominio("El código de barras es obligatorio");
        Categoria = categoria ?? throw new ExcepcionDominio("La categoría es obligatoria");
        Marca = marca;
        SetPrecioUnitario(precioUnitario);
        UnidadMedida = unidadMedida;
        EstaActivo = true;
        FechaCreacion = DateTime.UtcNow;
        ImagenUrl = imagenUrl;
        PuntoReorden = puntoReorden;
    }

    public void Actualizar(
        string nombre,
        string? descripcion,
        Categoria categoria,
        decimal precioUnitario,
        Marca? marca = null,
        string? imagenUrl = null,
        int puntoReorden = 10)
    {
        SetNombre(nombre);
        Descripcion = descripcion;
        Categoria = categoria ?? throw new ExcepcionDominio("La categoría es obligatoria");
        SetPrecioUnitario(precioUnitario);
        Marca = marca;
        ImagenUrl = imagenUrl;
        PuntoReorden = puntoReorden;
        FechaActualizacion = DateTime.UtcNow;
    }

    public void Activar() => EstaActivo = true;
    public void Desactivar() => EstaActivo = false;

    private void SetNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ExcepcionDominio("El nombre del producto no puede estar vacío");

        if (nombre.Length < 3)
            throw new ExcepcionDominio("El nombre del producto debe tener al menos 3 caracteres");

        if (nombre.Length > 100)
            throw new ExcepcionDominio("El nombre del producto no puede tener más de 100 caracteres");

        Nombre = nombre.Trim();
    }

    private void SetPrecioUnitario(decimal precio)
    {
        if (precio < 0)
            throw new ExcepcionDominio("El precio unitario no puede ser negativo");

        if (precio > 99999999.99m)
            throw new ExcepcionDominio("El precio unitario no puede superar 99,999,999.99");

        PrecioUnitario = Math.Round(precio, 2);
    }
}