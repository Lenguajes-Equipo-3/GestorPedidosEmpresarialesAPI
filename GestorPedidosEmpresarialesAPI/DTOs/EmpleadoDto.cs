namespace GestorPedidosEmpresarialesAPI.DTOs
{
    public class EmpleadoDto
    {
        public int IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; }
        public string ApellidosEmpleado { get; set; }
        public string Puesto { get; set; }
        public string Extension { get; set; }
        public string TelefonoTrabajo { get; set; }

        public DepartamentoDto Departamento { get; set; }
        public RolDto Rol { get; set; }
    }
}