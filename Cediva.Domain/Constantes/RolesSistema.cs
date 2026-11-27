namespace Cediva.Domain.Constantes
{
    public static class RolesSistema
    {
        public const string Administrador = "Administrador";
        public const string Supervisor = "Supervisor";
        public const string AuditorBodega = "AuditorBodega";
        public const string AuxiliarLogistico = "AuxiliarLogistico";

        // Agrupaciones estandarizadas para [Authorize(Roles = ...)]
        public const string Todos = $"{Administrador},{Supervisor},{AuditorBodega},{AuxiliarLogistico}";
        public const string MuelleOperativo = $"{Administrador},{Supervisor},{AuditorBodega},{AuxiliarLogistico}";
        public const string AuditoriaYSupervision = $"{Administrador},{Supervisor},{AuditorBodega}";
        public const string AdministracionYSupervision = $"{Administrador},{Supervisor}";
        public const string SoloAdministrador = Administrador;
    }
}
