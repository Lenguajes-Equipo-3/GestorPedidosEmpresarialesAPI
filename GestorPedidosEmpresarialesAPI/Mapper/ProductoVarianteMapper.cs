using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesBackend.Domain;

namespace GestorPedidosEmpresarialesAPI.Mapper
{
    public static class ProductoVarianteMapper
    {
        public static ProductoVarianteDto ToDto(VarianteProducto varianteProducto)
        {
            return new ProductoVarianteDto
            {
                IdVariante = varianteProducto.IdVariante,
                ProductoBase = ProductoBaseMapper.ToDto(varianteProducto.ProductoBase),
                Talla = varianteProducto.Talla,
                Descripcion = varianteProducto.Descripcion,
                Precio = varianteProducto.Precio,
                CantidadExistencias = varianteProducto.CantidadExistencias,
                PuntoReorden = varianteProducto.PuntoReorden
            };
        }

        public static VarianteProducto ToEntity(ProductoVarianteDto dto)
        {
            return new VarianteProducto(
                dto.IdVariante,
                ProductoBaseMapper.ToEntity(dto.ProductoBase),
                dto.Talla,
                dto.Descripcion,
                dto.Precio,
                dto.CantidadExistencias,
                dto.PuntoReorden,
                false // Por defecto, no eliminado
            );
        }
    }
}