using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesBackend.Domain;

namespace GestorPedidosEmpresarialesAPI.Mapper
{
    public static class ClienteMapper
    {
        public static ClienteDto ToDto(Cliente cliente)
        {
            return new ClienteDto
            {
                IdCliente = cliente.IdCliente,
                NombreCompania = cliente.NombreCompania,
                NombreContacto = cliente.NombreContacto,
                ApellidoContacto = cliente.ApellidoContacto,
                PuestoContacto = cliente.PuestoContacto,
                Direccion = cliente.Direccion,
                Ciudad = cliente.Ciudad,
                Provincia = cliente.Provincia,
                CodigoPostal = cliente.CodigoPostal,
                Pais = cliente.Pais,
                Telefono = cliente.Telefono
            };
        }

        public static Cliente ToEntity(ClienteDto dto)
        {
            return new Cliente
            {
                IdCliente = dto.IdCliente,
                NombreCompania = dto.NombreCompania,
                NombreContacto = dto.NombreContacto,
                ApellidoContacto = dto.ApellidoContacto,
                PuestoContacto = dto.PuestoContacto,
                Direccion = dto.Direccion,
                Ciudad = dto.Ciudad,
                Provincia = dto.Provincia,
                CodigoPostal = dto.CodigoPostal,
                Pais = dto.Pais,
                Telefono = dto.Telefono,
                Eliminado = false
            };
        }
    }
}
