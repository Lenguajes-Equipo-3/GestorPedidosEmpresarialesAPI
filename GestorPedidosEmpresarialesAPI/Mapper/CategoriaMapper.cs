using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesBackend.Domain;

namespace GestorPedidosEmpresarialesAPI.Mapper
{
    public static class CategoriaMapper
    {
        public static CategoriaDto ToDto(Categoria categoria)
        {
            return new CategoriaDto
            {
                IdCategoria = categoria.IdCategoria,
                Descripcion = categoria.Descripcion
            };
        }

        public static Categoria ToEntity(CategoriaDto dto)
        {
            return new Categoria(
                dto.IdCategoria,
                dto.Descripcion,
                false 
            );
        }
    }
}