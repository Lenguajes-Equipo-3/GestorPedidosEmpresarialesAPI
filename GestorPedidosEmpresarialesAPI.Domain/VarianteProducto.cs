using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Domain
{
    public class VarianteProducto
    {
        private int idVariante;
        private ProductoBase productoBase;
        private string talla;
        private string descripcion;
        private decimal precio;
        private decimal cantidadExistencias;
        private int puntoReorden;
        private bool eliminado;

        public VarianteProducto(int idVariante, ProductoBase productoBase, string talla, string descripcion, decimal precio, decimal cantidadExistencias, int puntoReorden, bool eliminado)
        {
            this.idVariante = idVariante;
            this.productoBase = productoBase;
            this.talla = talla;
            this.descripcion = descripcion;
            this.precio = precio;
            this.cantidadExistencias = cantidadExistencias;
            this.puntoReorden = puntoReorden;
            this.eliminado = eliminado;
        }

        

        public int IdVariante { get => idVariante; set => idVariante = value; }
        public ProductoBase ProductoBase { get => productoBase; set => productoBase = value; }
        public string Talla { get => talla; set => talla = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public decimal Precio { get => precio; set => precio = value; }
        public decimal CantidadExistencias { get => cantidadExistencias; set => cantidadExistencias = value; }
        public int PuntoReorden { get => puntoReorden; set => puntoReorden = value; }
        public bool Eliminado { get => eliminado; set => eliminado = value; }

    }
}
