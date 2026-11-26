namespace Cediva.Application.Comunes.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verificar(string password, string passwordHash);
    }
}
