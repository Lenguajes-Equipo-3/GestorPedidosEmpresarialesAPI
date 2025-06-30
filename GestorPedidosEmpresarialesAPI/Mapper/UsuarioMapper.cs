using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesBackend.Domain;

namespace GestorPedidosEmpresarialesAPI.Mapper
{
    public static class UsuarioMapper
    {
        public static UsuarioDto ToDto(Usuario usuario)
        {
            return new UsuarioDto
            {
                IdUsuario = usuario.IdUsuario,
                Email = usuario.Email,
                Empleado = EmpleadoMapper.ToDto(usuario.Empleado)
            };
        }

        public static Usuario ToEntity(UsuarioDto dto)
        {
            return new Usuario
            {
                IdUsuario = dto.IdUsuario,
                Email = dto.Email,
                Empleado = EmpleadoMapper.ToEntity(dto.Empleado),
                Contrasenna = "", // puede omitirse o controlarse desde otro DTO específico de autenticación
                Eliminado = false
            };
        }
    }
}