using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Domain
{

    using System;

    public class Orden
    {
        private int idOrden;
        private Cliente cliente;
        private Empleado empleado;
        private DateTime fechaOrden;
        private string direccionViaje;
        private string ciudadViaje;
        private string provinciaViaje;
        private string paisViaje;
        private string telefonoViaje;
        private bool eliminado;

        public Orden(int idOrden, Cliente cliente, Empleado empleado, DateTime fechaOrden,
                     string direccionViaje, string ciudadViaje, string provinciaViaje,
                     string paisViaje, string telefonoViaje, bool eliminado)
        {
            this.idOrden = idOrden;
            this.cliente = cliente;
            this.empleado = empleado;
            this.fechaOrden = fechaOrden;
            this.direccionViaje = direccionViaje;
            this.ciudadViaje = ciudadViaje;
            this.provinciaViaje = provinciaViaje;
            this.paisViaje = paisViaje;
            this.telefonoViaje = telefonoViaje;
            this.eliminado = eliminado;
        }

        public int IdOrden { get => idOrden; set => idOrden = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public Empleado Empleado { get => empleado; set => empleado = value; }
        public DateTime FechaOrden { get => fechaOrden; set => fechaOrden = value; }
        public string DireccionViaje { get => direccionViaje; set => direccionViaje = value; }
        public string CiudadViaje { get => ciudadViaje; set => ciudadViaje = value; }
        public string ProvinciaViaje { get => provinciaViaje; set => provinciaViaje = value; }
        public string PaisViaje { get => paisViaje; set => paisViaje = value; }
        public string TelefonoViaje { get => telefonoViaje; set => telefonoViaje = value; }
        public bool Eliminado { get => eliminado; set => eliminado = value; }
    }


}
