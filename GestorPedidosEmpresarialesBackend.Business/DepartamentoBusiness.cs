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
        public void Insert(Departamento departamento) => _departamentoData.Insert(departamento);
        public void Update(Departamento departamento) => _departamentoData.Update(departamento);
        public void Delete(int id) => _departamentoData.Delete(id);
    }
}