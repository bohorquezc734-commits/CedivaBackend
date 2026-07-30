namespace Cediva.Dominio.Excepciones;

public class ExcepcionDominio : Exception
{
    public ExcepcionDominio(string mensaje) : base(mensaje) { }

    public ExcepcionDominio(string mensaje, Exception innerException)
        : base(mensaje, innerException) { }
}