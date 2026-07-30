using Cediva.Dominio.Excepciones;

namespace Cediva.Dominio.ObjetosValor;

public sealed record CodigoBarras
{
    public string Valor { get; }

    private const int LongitudMinima = 8;
    private const int LongitudMaxima = 20;

    public CodigoBarras(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ExcepcionDominio("El código de barras no puede estar vacío");

        valor = valor.Trim();

        if (valor.Length < LongitudMinima || valor.Length > LongitudMaxima)
            throw new ExcepcionDominio($"El código de barras debe tener entre {LongitudMinima} y {LongitudMaxima} caracteres");

        if (!EsValido(valor))
            throw new ExcepcionDominio("El código de barras contiene caracteres no válidos");

        Valor = valor;
    }

    private static bool EsValido(string valor)
    {
        // Solo permite números, letras mayúsculas y guiones
        return System.Text.RegularExpressions.Regex.IsMatch(valor, @"^[A-Z0-9\-]+$");
    }

    public override string ToString() => Valor;
}