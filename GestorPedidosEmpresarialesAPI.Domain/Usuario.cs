using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Domain
{
    public class Usuario
    {
        private int idUsuario;
        private  Empleado empleado;
        private string email;
        private string contrasenna;
        private bool eliminado;

       Usuario(){ }

        public Usuario(int idUsuario, Empleado empleado, string email, string contrasenna, bool eliminado)
        {
            this.idUsuario = idUsuario;
            this.empleado = empleado;
            this.email = email;
            this.contrasenna = contrasenna;
            this.eliminado = eliminado;
        }
        public int IdUsuario { get => idUsuario; set => idUsuario = value; }
        public Empleado Empleado { get => empleado; set => empleado = value; }
        public string Email { get => email; set => email = value; }
        public string Contrasenna { get => contrasenna; set => contrasenna = value; }
        public bool Eliminado { get => eliminado; set => eliminado = value; }


    }
}
