using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Domain
{
    public class ProductoBase
    {
        private int idProductoBase;
        private string nombreProducto;
        private int idCategoria;
        private bool eliminado;

        public ProductoBase(int idProductoBase, string nombreProducto, int idCategoria, bool eliminado)
        {
            this.idProductoBase = idProductoBase;
            this.nombreProducto = nombreProducto;
            this.idCategoria = idCategoria;
            this.eliminado = eliminado;
        }

        public int IdProductoBase { get => idProductoBase; set => idProductoBase = value; }
        public string NombreProducto { get => nombreProducto; set => nombreProducto = value; }
        public int IdCategoria { get => idCategoria; set => idCategoria = value; }
        public bool Eliminado { get => eliminado; set => eliminado = value; }

    }
}
