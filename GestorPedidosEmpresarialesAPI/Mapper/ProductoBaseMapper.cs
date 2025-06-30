using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesBackend.Domain;

namespace GestorPedidosEmpresarialesAPI.Mapper
{
    public static class ProductoBaseMapper
    {
        public static ProductoBaseDto ToDto(ProductoBase productoBase)
        {
            return new ProductoBaseDto
            {
                IdProductoBase = productoBase.IdProductoBase,
                NombreProducto = productoBase.NombreProducto,
                Categoria = CategoriaMapper.ToDto(productoBase.Categoria)
            };
        }

        public static ProductoBase ToEntity(ProductoBaseDto dto)
        {
            return new ProductoBase(
                dto.IdProductoBase,
                dto.NombreProducto,
                new Categoria(
                    dto.Categoria.IdCategoria,
                    dto.Categoria.Descripcion,
                    false 
                ),
                false 
            );
        }
    }
}