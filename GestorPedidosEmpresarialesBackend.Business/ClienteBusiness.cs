using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorPedidosEmpresarialesBackend.Data;
using GestorPedidosEmpresarialesBackend.Domain;

namespace GestorPedidosEmpresarialesBackend.Business
{
    public class ClienteBusiness
    {
        private readonly ClienteData clienteData;

        public ClienteBusiness(ClienteData clienteData)
        {
            this.clienteData = clienteData;
        }

        public List<Cliente> GetAllClientes() => clienteData.GetAllClientes();

        public void AddCliente(Cliente cliente) => clienteData.Insert(cliente);

        public Cliente? GetClienteById(int id) => clienteData.GetClienteById(id);

        public void UpdateCliente(Cliente cliente) => clienteData.UpdateCliente(cliente);

        public void DeleteCliente(int id) => clienteData.DeleteCliente(id);
    }
}
