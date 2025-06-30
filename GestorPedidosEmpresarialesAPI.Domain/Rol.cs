using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Domain
{
    public class Rol
    {
        private int idRol;
        private string nombreRol;
        private bool eliminado;

        public Rol(int idRol, string nombreRol, bool eliminado)
        {
            this.idRol = idRol;
            this.nombreRol = nombreRol;
            this.eliminado = eliminado;
        }

        public int IdRol { get => idRol; set => idRol = value; }
        public string NombreRol { get => nombreRol; set => nombreRol = value; }
        public bool Eliminado { get => eliminado; set => eliminado = value; }

    }
}
