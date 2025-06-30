using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesBackend.Domain;

namespace GestorPedidosEmpresarialesAPI.Mapper
{
    public static class RolMapper
    {
        public static RolDto ToDto(Rol rol)
        {
            return new RolDto
            {
                IdRol = rol.IdRol,
                NombreRol = rol.NombreRol
            };
        }

        public static Rol ToEntity(RolDto dto)
        {
            return new Rol
            {
                IdRol = dto.IdRol,
                NombreRol = dto.NombreRol,
                Eliminado = false // Por defecto en inserción
            };
        }
    }
}
