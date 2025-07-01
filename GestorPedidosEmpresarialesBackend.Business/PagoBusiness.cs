using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorPedidosEmpresarialesBackend.Data;
using GestorPedidosEmpresarialesBackend.Domain;

namespace GestorPedidosEmpresarialesBackend.Business
{
    public class PagoBusiness
    {
        private readonly PagoData data;

        public PagoBusiness(PagoData data)
        {
            this.data = data;
        }

        public List<Pago> GetAll() => data.GetAll();

        public void Insert(Pago pago) => data.Insert(pago);

        
    }
}
