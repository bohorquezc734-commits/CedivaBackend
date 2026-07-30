using Cediva.Dominio.Excepciones;

namespace Cediva.Dominio.ObjetosValor;

public sealed record Direccion
{
    public string Calle { get; }
    public string? Numero { get; }
    public string? Complemento { get; }
    public string Ciudad { get; }
    public string? Departamento { get; }
    public string CodigoPostal { get; }
    public string Pais { get; }

    public Direccion(
        string calle,
        string? numero,
        string? complemento,
        string ciudad,
        string? departamento,
        string codigoPostal,
        string pais)
    {
        if (string.IsNullOrWhiteSpace(calle))
            throw new ExcepcionDominio("La calle no puede estar vacía");

        if (string.IsNullOrWhiteSpace(ciudad))
            throw new ExcepcionDominio("La ciudad no puede estar vacía");

        if (string.IsNullOrWhiteSpace(codigoPostal))
            throw new ExcepcionDominio("El código postal no puede estar vacío");

        if (string.IsNullOrWhiteSpace(pais))
            throw new ExcepcionDominio("El país no puede estar vacío");

        Calle = calle.Trim();
        Numero = numero?.Trim();
        Complemento = complemento?.Trim();
        Ciudad = ciudad.Trim();
        Departamento = departamento?.Trim();
        CodigoPostal = codigoPostal.Trim();
        Pais = pais.Trim();
    }

    public override string ToString()
    {
        var partes = new List<string> { Calle };

        if (!string.IsNullOrEmpty(Numero))
            partes.Add($"#{Numero}");

        if (!string.IsNullOrEmpty(Complemento))
            partes.Add($"- {Complemento}");

        partes.Add(Ciudad);

        if (!string.IsNullOrEmpty(Departamento))
            partes.Add(Departamento);

        partes.Add(CodigoPostal);
        partes.Add(Pais);

        return string.Join(", ", partes);
    }
}