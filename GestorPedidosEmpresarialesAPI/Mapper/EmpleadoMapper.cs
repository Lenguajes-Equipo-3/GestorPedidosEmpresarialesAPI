using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesBackend.Domain;

namespace GestorPedidosEmpresarialesAPI.Mapper
{
    public static class EmpleadoMapper
    {
        public static EmpleadoDto ToDto(Empleado empleado)
        {
            return new EmpleadoDto
            {
                IdEmpleado = empleado.IdEmpleado,
                NombreEmpleado = empleado.NombreEmpleado,
                ApellidosEmpleado = empleado.ApellidosEmpleado,
                Puesto = empleado.Puesto,
                Extension = empleado.Extension,
                TelefonoTrabajo = empleado.TelefonoTrabajo,
                Departamento = DepartamentoMapper.ToDto(empleado.Departamento),
                Rol = RolMapper.ToDto(empleado.Rol)
            };
        }

        public static Empleado ToEntity(EmpleadoDto dto)
        {
            return new Empleado
            {
                IdEmpleado = dto.IdEmpleado,
                NombreEmpleado = dto.NombreEmpleado,
                ApellidosEmpleado = dto.ApellidosEmpleado,
                Puesto = dto.Puesto,
                Extension = dto.Extension,
                TelefonoTrabajo = dto.TelefonoTrabajo,
                Departamento = new Departamento
                {
                    IdDepartamento = dto.Departamento.IdDepartamento,
                    NombreDepartamento = dto.Departamento.NombreDepartamento
                },
                Rol = new Rol
                {
                    IdRol = dto.Rol.IdRol,
                    NombreRol = dto.Rol.NombreRol
                },
                Eliminado = false
            };
        }
    }
}
 