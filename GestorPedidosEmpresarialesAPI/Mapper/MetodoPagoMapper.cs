using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesBackend.Domain;

namespace GestorPedidosEmpresarialesAPI.Mapper
{
    public static class MetodoPagoMapper
    {
        public static MetodoPagoDto ToDto(MetodoPago m) => new()
        {
            IdMetodoPago = m.IdMetodoPago,
            Metodo = m.Metodo,
            TarjetaCredito = m.TarjetaCredito
        };
    }
}
