using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Domain
{
    public class DetalleOrden
    {
        private Orden orden;
        private VarianteProducto varianteProducto;
        private int cantidad;
        private double precioLinea;
        private bool eliminado;

        public DetalleOrden(Orden orden, VarianteProducto varianteProducto, int cantidad, double precioLinea, bool eliminado)
        {
            this.orden = orden;
            this.varianteProducto = varianteProducto;
            this.cantidad = cantidad;
            this.precioLinea = precioLinea;
            this.eliminado = eliminado;
        }

        public Orden Orden { get => orden; set => orden = value; }
        public VarianteProducto VarianteProducto { get => varianteProducto; set => varianteProducto = value; }
        public int Cantidad { get => cantidad; set => cantidad = value; }
        public double PrecioLinea { get => precioLinea; set => precioLinea = value; }
        public bool Eliminado { get => eliminado; set => eliminado = value; }
    }

}
