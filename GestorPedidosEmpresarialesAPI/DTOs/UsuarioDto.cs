namespace GestorPedidosEmpresarialesAPI.DTOs
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }

        public EmpleadoDto Empleado { get; set; }
    }
}
