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
        private Categoria categoria;
        private bool eliminado;

        public ProductoBase(int idProductoBase, string nombreProducto, Categoria categoria, bool eliminado)
        {
            this.idProductoBase = idProductoBase;
            this.nombreProducto = nombreProducto;
            this.categoria = categoria;
            this.eliminado = eliminado;
        }

        public int IdProductoBase { get => idProductoBase; set => idProductoBase = value; }
        public string NombreProducto { get => nombreProducto; set => nombreProducto = value; }
        public Categoria Categoria { get => categoria; set => categoria = value; }
        public bool Eliminado { get => eliminado; set => eliminado = value; }

    }
}
