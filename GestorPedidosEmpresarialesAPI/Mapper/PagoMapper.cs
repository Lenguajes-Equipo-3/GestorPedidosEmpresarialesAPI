using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesBackend.Domain;

namespace GestorPedidosEmpresarialesAPI.Mapper
{
    public static class PagoMapper
    {
        public static PagoDto ToDto(Pago pago) => new()
        {
            IdPago = pago.IdPago,
            CantidadPago = pago.CantidadPago,
            FechaPago = pago.FechaPago,
            NumTarjetaCredito = pago.NumTarjetaCredito,
            NomUsuarioTarjeta = pago.NomUsuarioTarjeta,
            Orden = OrdenMapper.ToDto(pago.Orden),
            MetodoPago = MetodoPagoMapper.ToDto(pago.MetodoPago)
        };

        public static Pago ToEntity(PagoDto dto) => new(
            idPago: dto.IdPago,
            orden: OrdenMapper.ToEntity(dto.Orden),
            cantidadPago: dto.CantidadPago,
            fechaPago: dto.FechaPago,
            numTarjetaCredito: dto.NumTarjetaCredito,
            nomUsuarioTarjeta: dto.NomUsuarioTarjeta,
            metodoPago: MetodoPagoMapper.ToEntity(dto.MetodoPago),
            eliminado: false
        );
    }
}
