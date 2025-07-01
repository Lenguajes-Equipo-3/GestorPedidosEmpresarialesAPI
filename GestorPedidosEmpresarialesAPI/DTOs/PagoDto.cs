namespace GestorPedidosEmpresarialesAPI.DTOs
{
    public class PagoDto
    {
        public int IdPago { get; set; }
        public OrdenDto Orden { get; set; }
        public double CantidadPago { get; set; }
        public DateTime FechaPago { get; set; }
        public string NumTarjetaCredito { get; set; }
        public string NomUsuarioTarjeta { get; set; }
        public MetodoPagoDto MetodoPago { get; set; }
    }
}
