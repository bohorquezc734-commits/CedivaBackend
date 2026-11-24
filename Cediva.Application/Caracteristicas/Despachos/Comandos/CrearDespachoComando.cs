using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cediva.Domain.Agregados.Inventario;
using Cediva.Domain.Agregados.Despacho;
using Cediva.Dominio.Interfaces;
using Cediva.Dominio.ObjetosValor;
using Cediva.Domain.Enumeraciones;
using Cediva.Domain.Excepciones;

namespace Cediva.Application.Caracteristicas.Despachos.Comandos
{
   
    public record ItemDespachoDto(Guid ProductoId, int Cantidad);

    public record CrearDespachoComando(
        string ClienteNombre,
        List<ItemDespachoDto> Items
    ) : IRequest<Guid>;

    
    public class CrearDespachoHandler : IRequestHandler<CrearDespachoComando, Guid>
    {
        private readonly IRepositorioDespacho _repositorioDespacho;
        private readonly IRepositorioInventario _repositorioInventario;
        private readonly IRepositorioProducto _repositorioProducto;

        public CrearDespachoHandler(
            IRepositorioDespacho repositorioDespacho,
            IRepositorioInventario repositorioInventario,
            IRepositorioProducto repositorioProducto)
        {
            _repositorioDespacho = repositorioDespacho;
            _repositorioInventario = repositorioInventario;
            _repositorioProducto = repositorioProducto;
        }

        public Guid despachoId { get; private set; }

        public async Task<Guid> Handle(CrearDespachoComando request, CancellationToken cancellationToken)
        {
           
            var direccionPorDefecto = new Direccion(
                "Calle Falsa",
                "123",          
                "Local 4",        
                "Bogotá",      
                "Cundinamarca",    
                "111111",        
                "Colombia"        
            );

           
            var despacho = new Despacho(
                despachoId,
                request.ClienteNombre,
                DateTime.Now,
                EstadoDespacho.Pendiente,
                direccionPorDefecto 
            );
            
            

            foreach (var item in request.Items)
            {
                var producto = await _repositorioProducto.ObtenerPorId(item.ProductoId, cancellationToken);
                if (producto == null) throw new ReglaDeNegocioException($"El producto no existe.");

                var inventario = await _repositorioInventario.ObtenerPorProducto(item.ProductoId, cancellationToken);
                if (inventario == null || inventario.CantidadDisponible.Valor < item.Cantidad)
                {
                    throw new ReglaDeNegocioException($"Stock insuficiente para {producto.Nombre}. Stock actual: {inventario?.CantidadDisponible.Valor ?? 0}");
                }

                var nuevaCantidad = inventario.CantidadDisponible.Valor - item.Cantidad;
                inventario.ActualizarCantidad(new Cantidad(nuevaCantidad, inventario.CantidadDisponible.Unidad));

                despacho.AgregarItem(new ItemDespacho(
                    Guid.NewGuid(),
                    item.ProductoId,
                    new Cantidad(item.Cantidad, inventario.CantidadDisponible.Unidad)
                ));
            }

            await _repositorioDespacho.Agregar(despacho, cancellationToken);
            await _repositorioDespacho.GuardarCambios(despacho, cancellationToken);

            return despachoId;
        }
    }
}