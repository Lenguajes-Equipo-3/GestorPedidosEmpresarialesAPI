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
        private readonly List<DetalleOrden> detallesOrden = new List<DetalleOrden>();
        private DateTime fechaOrden;
        private string direccionViaje;
        private string ciudadViaje;
        private string provinciaViaje;
        private string paisViaje;
        private string telefonoViaje;
        private bool eliminado;

        //Crea orden sin detalles
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

        //Crea orden con detalles
        public Orden(int idOrden, Cliente cliente, Empleado empleado, List<DetalleOrden> detallesOrden,
                     DateTime fechaOrden, string direccionViaje, string ciudadViaje,
                     string provinciaViaje, string paisViaje, string telefonoViaje, bool eliminado)
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
            if (detallesOrden != null)
                this.detallesOrden = detallesOrden;
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

        // Propiedad de solo lectura para ver los detalles de forma segura
        public IReadOnlyList<DetalleOrden> DetallesOrden => detallesOrden.AsReadOnly();

        //Metodos de manipulacion de detalles de la orden
        public void AgregarDetalle(DetalleOrden detalle)
        {
            if (detalle != null)
            {
                this.detallesOrden.Add(detalle);
            }
        }
        public void EliminarDetalle(DetalleOrden detalle)
        {
            if (detalle != null)
            {
                this.detallesOrden.Remove(detalle);
            }
        }
    }
}
