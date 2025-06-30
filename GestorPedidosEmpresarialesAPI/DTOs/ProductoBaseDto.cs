using GestorPedidosEmpresarialesAPI.DTOs;

namespace GestorPedidosEmpresarialesAPI.DTOs
{
    public class ProductoBaseDto
    {
        public int IdProductoBase { get; set; }
        public string NombreProducto { get; set; }
        public CategoriaDto Categoria { get; set; }
    }
}