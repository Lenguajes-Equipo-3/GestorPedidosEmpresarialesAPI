using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesBackend.Domain;

namespace GestorPedidosEmpresarialesAPI.Mapper
{
    public static class DepartamentoMapper
    {
        public static DepartamentoDto ToDto(Departamento departamento)
        {
            return new DepartamentoDto
            {
                IdDepartamento = departamento.IdDepartamento,
                NombreDepartamento = departamento.NombreDepartamento
            };
        }

        public static Departamento ToEntity(DepartamentoDto dto)
        {
            return new Departamento
            {
                IdDepartamento = dto.IdDepartamento,
                NombreDepartamento = dto.NombreDepartamento,
                Eliminado = false
            };
        }
    }
}

