using GestorPedidosEmpresarialesBackend.Data;
using GestorPedidosEmpresarialesBackend.Domain;
using System.Collections.Generic;

namespace GestorPedidosEmpresarialesBackend.Business
{
    public class DepartamentoBusiness
    {
        private readonly DepartamentoData _departamentoData;

        public DepartamentoBusiness(DepartamentoData departamentoData)
        {
            _departamentoData = departamentoData;
        }

        public List<Departamento> GetAll() => _departamentoData.GetAll();
        public Departamento GetById(int id) => _departamentoData.GetById(id);
    }
}