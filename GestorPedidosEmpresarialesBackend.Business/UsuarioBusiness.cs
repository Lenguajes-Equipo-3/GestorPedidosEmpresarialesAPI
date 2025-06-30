using GestorPedidosEmpresarialesBackend.Data;
using GestorPedidosEmpresarialesBackend.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Business
{
    public class UsuarioBusiness
    {
        private readonly UsuarioData usuarioData;

        public UsuarioBusiness(UsuarioData usuarioData)
        {
            this.usuarioData = usuarioData;
        }

        public Usuario ValidarLogin(string correo, string contrasenna)
        {
            try
            {
                var usuario = usuarioData.ValidarLogin(correo, contrasenna);
                if (usuario == null)
                {
                    throw new UnauthorizedAccessException("Credenciales inválidas.");
                }
                return usuario;
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al acceder a la base de datos.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error inesperado al validar login.", ex);
            }
        }
    }
}
✅
