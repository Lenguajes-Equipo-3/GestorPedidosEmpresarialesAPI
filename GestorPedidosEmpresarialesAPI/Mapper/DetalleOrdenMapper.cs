using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesBackend.Domain;

namespace GestorPedidosEmpresarialesAPI.Mapper
{
    public static class DetalleOrdenMapper
    {
        public static DetalleOrdenDto ToDto(DetalleOrden detalle) => new()
        {
            IdDetalleOrden = detalle.IdDetalleOrden,
            VarianteProducto =  ProductoVarianteMapper.ToDto(detalle.VarianteProducto),
            Cantidad = detalle.Cantidad,
            PrecioLinea = detalle.PrecioLinea
        };

        public static DetalleOrden ToEntity(DetalleOrdenDto dto) => new(
            idDetalleOrden: dto.IdDetalleOrden,
            varianteProducto: ProductoVarianteMapper.ToEntity(dto.VarianteProducto),
            cantidad: dto.Cantidad,
            precioLinea: dto.PrecioLinea,
            eliminado: false
        );
    }
}
