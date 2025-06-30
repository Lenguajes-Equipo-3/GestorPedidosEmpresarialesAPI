using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Domain
{
    public class Categoria
    {
        private int idCategoria;
        private string descripcion;
        private bool eliminado;

        public Categoria(int idCategoria, string descripcion, bool eliminado)
        {
            this.idCategoria = idCategoria;
            this.descripcion = descripcion;
            this.eliminado = eliminado;
        }

        public int IdCategoria { get => idCategoria; set => idCategoria = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public bool Eliminado { get => eliminado; set => eliminado = value; }
    }
}
