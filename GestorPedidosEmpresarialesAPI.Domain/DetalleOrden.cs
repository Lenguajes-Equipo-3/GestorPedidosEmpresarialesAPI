using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Domain
{
    public class DetalleOrden
    {
        private int idDetalleOrden;
        private VarianteProducto varianteProducto;
        private int cantidad;
        private double precioLinea;
        private bool eliminado;

        public DetalleOrden(int idDetalleOrden, VarianteProducto varianteProducto, int cantidad, double precioLinea, bool eliminado)
        {
            this.idDetalleOrden = idDetalleOrden;
            this.varianteProducto = varianteProducto;
            this.cantidad = cantidad;
            this.precioLinea = precioLinea;
            this.eliminado = eliminado;
        }

        public int IdDetalleOrden { get => idDetalleOrden; set => idDetalleOrden = value; }
        public VarianteProducto VarianteProducto { get => varianteProducto; set => varianteProducto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public double PrecioLinea { get => precioLinea; set => precioLinea = value; }
        public bool Eliminado { get => eliminado; set => eliminado = value; }
    }

}
