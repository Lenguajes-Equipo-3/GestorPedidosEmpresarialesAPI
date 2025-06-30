namespace GestorPedidosEmpresarialesAPI.DTOs
{
    public class ClienteDto
    {
        public int IdCliente { get; set; }
        public string NombreCompania { get; set; }
        public string NombreContacto { get; set; }
        public string ApellidoContacto { get; set; }
        public string PuestoContacto { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public string CodigoPostal { get; set; }
        public string Pais { get; set; }
        public string Telefono { get; set; }
    }
}
