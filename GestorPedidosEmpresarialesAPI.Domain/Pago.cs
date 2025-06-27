using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Domain
{
    using System;

    public class Pago
    {
        private int idPago;
        private int idOrden;
        private double cantidadPago;
        private DateTime fechaPago;
        private string numTarjetaCredito;
        private string nomUsuarioTarjeta;
        private MetodoPago metodoPago;
        private bool eliminado;

        public Pago(int idPago, int idOrden, double cantidadPago, DateTime fechaPago,
                    string numTarjetaCredito, string nomUsuarioTarjeta,
                    MetodoPago metodoPago, bool eliminado)
        {
            this.idPago = idPago;
            this.idOrden = idOrden;
            this.cantidadPago = cantidadPago;
            this.fechaPago = fechaPago;
            this.numTarjetaCredito = numTarjetaCredito;
            this.nomUsuarioTarjeta = nomUsuarioTarjeta;
            this.metodoPago = metodoPago;
            this.eliminado = eliminado;
        }

        public int IdPago { get => idPago; set => idPago = value; }
        public int IdOrden { get => idOrden; set => idOrden = value; }
        public double CantidadPago { get => cantidadPago; set => cantidadPago = value; }
        public DateTime FechaPago { get => fechaPago; set => fechaPago = value; }
        public string NumTarjetaCredito { get => numTarjetaCredito; set => numTarjetaCredito = value; }
        public string NomUsuarioTarjeta { get => nomUsuarioTarjeta; set => nomUsuarioTarjeta = value; }
        public MetodoPago MetodoPago { get => metodoPago; set => metodoPago = value; }
        public bool Eliminado { get => eliminado; set => eliminado = value; }
    }

}
