using Cediva.Domain.Enumeraciones;

using Cediva.Dominio.Excepciones;

namespace Cediva.Dominio.ObjetosValor;

public sealed record Cantidad
{
    public decimal Valor { get; }
    public UnidadMedida Unidad { get; }

    public Cantidad(decimal valor, UnidadMedida unidad)
    {
        if (valor < 0)
            throw new ExcepcionDominio("La cantidad no puede ser negativa");

        if (valor > 999999.99m)
            throw new ExcepcionDominio("La cantidad no puede superar 999,999.99");

        Valor = Math.Round(valor, 2);
        Unidad = unidad;
    }

    public static Cantidad Cero(UnidadMedida unidad) => new(0, unidad);

    public Cantidad Sumar(Cantidad otra)
    {
        if (otra.Unidad != Unidad)
            throw new ExcepcionDominio($"No se pueden sumar cantidades con diferentes unidades: {Unidad} vs {otra.Unidad}");

        return new Cantidad(Valor + otra.Valor, Unidad);
    }

    public Cantidad Restar(Cantidad otra)
    {
        if (otra.Unidad != Unidad)
            throw new ExcepcionDominio($"No se pueden restar cantidades con diferentes unidades: {Unidad} vs {otra.Unidad}");

        if (otra.Valor > Valor)
            throw new ExcepcionDominio("No se puede restar una cantidad mayor a la disponible");

        return new Cantidad(Valor - otra.Valor, Unidad);
    }

    public override string ToString() => $"{Valor} {Unidad}";
}