using GestorPedidosEmpresarialesAPI.DTOs;

namespace GestorPedidosEmpresarialesAPI.DTOs
{
    public class ProductoVarianteDto
    {
        public int IdVariante { get; set; }
        public ProductoBaseDto ProductoBase { get; set; }
        public string Talla { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public decimal CantidadExistencias { get; set; }
        public int PuntoReorden { get; set; }
    }
}