using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Domain
{
    public class MetodoPago
    {
        private int idMetodoPago;
        private string metodoPago;
        private string tarjetaCredito;
        private bool eliminado;

        public MetodoPago() { }
        public MetodoPago(int idMetodoPago, string metodoPago, string tarjetaCredito, bool eliminado)
        {
            this.idMetodoPago = idMetodoPago;
            this.metodoPago = metodoPago;
            this.tarjetaCredito = tarjetaCredito;
            this.eliminado = eliminado;
        }

        public int IdMetodoPago { get => idMetodoPago; set => idMetodoPago = value; }
        public string Metodo { get => metodoPago; set => metodoPago = value; }
        public string TarjetaCredito { get => tarjetaCredito; set => tarjetaCredito = value; }
        public bool Eliminado { get => eliminado; set => eliminado = value; }
    }

}
