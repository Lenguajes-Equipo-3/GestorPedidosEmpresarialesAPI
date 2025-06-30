using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Domain
{
    public class Empleado
    {
        private int idEmpleado;
        private string nombreEmpleado;
        private string apellidosEmpleado;
        private string puesto;
        private string extension;
        private string telefonoTrabajo;
        private Departamento departamento;
        private Rol rol;
        private bool eliminado;

        public Empleado(int idEmpleado, string nombreEmpleado, string apellidosEmpleado, string puesto, string extension, string telefonoTrabajo, Departamento departamento, Rol rol, bool eliminado)
        {
            this.idEmpleado = idEmpleado;
            this.nombreEmpleado = nombreEmpleado;
            this.apellidosEmpleado = apellidosEmpleado;
            this.puesto = puesto;
            this.extension = extension;
            this.telefonoTrabajo = telefonoTrabajo;
            this.departamento = departamento;
            this.rol = rol;
            this.eliminado = eliminado;
        }

        public int IdEmpleado { get => idEmpleado; set => idEmpleado = value; }
        public string NombreEmpleado { get => nombreEmpleado; set => nombreEmpleado = value; }
        public string ApellidosEmpleado { get => apellidosEmpleado; set => apellidosEmpleado = value; }
        public string Puesto { get => puesto; set => puesto = value; }
        public string Extension { get => extension; set => extension = value; }
        public string TelefonoTrabajo { get => telefonoTrabajo; set => telefonoTrabajo = value; }
        public Departamento Departamento { get => departamento; set => departamento = value; }
        public Rol IdRol { get => rol; set => rol = value; }
        public bool Eliminado { get => eliminado; set => eliminado = value; }

    }
}
