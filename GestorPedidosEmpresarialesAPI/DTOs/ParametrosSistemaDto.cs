namespace GestorPedidosEmpresarialesAPI.DTOs
{
    public class ParametrosSistemaDto
    {
        public int IdParametro { get; set; }
        public string NombreEmpresa { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public decimal ImpuestoVentas { get; set; }
        public string Moneda { get; set; }
        public DateTime UltimaActualizacion { get; set; }
        public byte[]? Logo { get; set; }
    }
}
