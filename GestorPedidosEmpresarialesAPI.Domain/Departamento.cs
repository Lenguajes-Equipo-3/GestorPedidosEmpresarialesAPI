using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Domain
{
    public class Departamento
    {
        private int idDepartamento;
        private String nombreDepartamento;
        private bool eliminado;

        public Departamento(int idDepartamento, string nombreDepartamento, bool eliminado)
        {
            this.idDepartamento = idDepartamento;
            this.nombreDepartamento = nombreDepartamento;
            this.eliminado = eliminado;
        }

        public int IdDepartamento { get => idDepartamento; set => idDepartamento = value; }
        public string NombreDepartamento { get => nombreDepartamento; set => nombreDepartamento = value; }
        public bool Eliminado { get => eliminado; set => eliminado = value; }

    }
}
