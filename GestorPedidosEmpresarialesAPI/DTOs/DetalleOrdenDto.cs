namespace GestorPedidosEmpresarialesAPI.DTOs
{
    public class DetalleOrdenDto
    {
        public int IdDetalleOrden { get; set; }
        public ProductoVarianteDto VarianteProducto { get; set; }
        public int Cantidad { get; set; }
        public double PrecioLinea { get; set; }
    }
}
