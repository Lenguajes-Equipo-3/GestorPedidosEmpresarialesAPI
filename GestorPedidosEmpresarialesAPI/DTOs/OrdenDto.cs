using System;

namespace GestorPedidosEmpresarialesAPI.DTOs
{
    public class OrdenDto
    {
        public int IdOrden { get; set; }
        public ClienteDto Cliente { get; set; }
        public EmpleadoDto Empleado { get; set; }
       // public List<DetalleOrdenDto> DetallesOrden { get; set; }
        public DateTime FechaOrden { get; set; }
        public string DireccionViaje { get; set; }
        public string CiudadViaje { get; set; }
        public string ProvinciaViaje { get; set; }
        public string PaisViaje { get; set; }
        public string TelefonoViaje { get; set; }
    }
}
