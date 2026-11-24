using System;

namespace Cediva.Application.DTOs 
{
    
    public class ProductoDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string ? Descripcion { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public string CodigoBarras { get; set; } = string.Empty;

       
    }
}