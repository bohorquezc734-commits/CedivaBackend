namespace Cediva.Domain.Constantes
{
    public static class LimitesDominio
    {
        public static class Usuario
        {
            public const int LongitudMinimaNombre = 3;
            public const int LongitudMinimaPassword = 6;
        }

        public static class Catalogo
        {
            public const int LongitudMinimaNombreCategoria = 3;
            public const int LongitudMaximaNombreCategoria = 50;

            public const int LongitudMinimaNombreMarca = 2;
            public const int LongitudMaximaNombreMarca = 50;

            public const int LongitudMinimaNombreProducto = 3;
            public const int LongitudMaximaNombreProducto = 100;
        }
    }
}
