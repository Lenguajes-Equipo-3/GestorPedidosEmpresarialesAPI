using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesBackend.Domain;

namespace GestorPedidosEmpresarialesAPI.Mapper
{
    public static class OrdenMapper
    {
        public static OrdenDto ToDto(Orden orden) => new()
        {
            IdOrden = orden.IdOrden,
            Cliente = ClienteMapper.ToDto(orden.Cliente),
            Empleado = EmpleadoMapper.ToDto(orden.Empleado),
            DetallesOrden = orden.DetallesOrden.Select(DetalleOrdenMapper.ToDto).ToList(),
            FechaOrden = orden.FechaOrden,
            DireccionViaje = orden.DireccionViaje,
            CiudadViaje = orden.CiudadViaje,
            ProvinciaViaje = orden.ProvinciaViaje,
            PaisViaje = orden.PaisViaje,
            TelefonoViaje = orden.TelefonoViaje
        };

        public static Orden ToEntity(OrdenDto dto)
        {
            var orden = new Orden(
                idOrden: dto.IdOrden,
                cliente: ClienteMapper.ToEntity(dto.Cliente),
                empleado: EmpleadoMapper.ToEntity(dto.Empleado),
                detallesOrden: dto.DetallesOrden.Select(DetalleOrdenMapper.ToEntity).ToList(),
                fechaOrden: dto.FechaOrden,
                direccionViaje: dto.DireccionViaje,
                ciudadViaje: dto.CiudadViaje,
                provinciaViaje: dto.ProvinciaViaje,
                paisViaje: dto.PaisViaje,
                telefonoViaje: dto.TelefonoViaje,
                eliminado: false
            );

            return orden;
        }
    }
}
