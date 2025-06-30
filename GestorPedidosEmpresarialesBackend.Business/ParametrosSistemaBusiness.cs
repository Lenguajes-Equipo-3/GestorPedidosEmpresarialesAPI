using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorPedidosEmpresarialesBackend.Data;
using GestorPedidosEmpresarialesBackend.Domain;

namespace GestorPedidosEmpresarialesBackend.Business
{
    public class ParametrosSistemaBusiness
    {
        private readonly ParametrosSistemaData data;

        public ParametrosSistemaBusiness(ParametrosSistemaData data)
        {
            this.data = data;
        }

        public void AddParametro(ParametrosSistema parametros) => data.AddParametro(parametros);

        public ParametrosSistema? GetParametros() => data.GetParametros();

        public void UpdateParametros(ParametrosSistema parametros) => data.UpdateParametros(parametros);
    }
}
