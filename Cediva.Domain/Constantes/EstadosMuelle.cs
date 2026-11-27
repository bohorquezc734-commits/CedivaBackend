namespace Cediva.Domain.Constantes
{
    public static class EstadosMuelle
    {
        public const string PorAlistar = "por_alistar";
        public const string Alistada = "alistada";
        public const string Auditada = "auditada";
        public const string Despachada = "despachada";

        // Alias de compatibilidad hacia atrás
        public const string PendienteLegacy = "pendiente";
        public const string AuditadaLegacy = "auditada_lista_despacho";
    }
}
