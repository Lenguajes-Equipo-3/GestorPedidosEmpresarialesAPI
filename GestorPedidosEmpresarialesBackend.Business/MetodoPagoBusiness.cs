using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorPedidosEmpresarialesBackend.Data;
using GestorPedidosEmpresarialesBackend.Domain;

namespace GestorPedidosEmpresarialesBackend.Business
{
    public class MetodoPagoBusiness
    {
        private readonly MetodoPagoData data;

        public MetodoPagoBusiness(MetodoPagoData data)
        {
            this.data = data;
        }

        public List<MetodoPago> GetAll() => data.GetAll();

        public MetodoPago? GetById(int id) => data.GetById(id);
    }
}
