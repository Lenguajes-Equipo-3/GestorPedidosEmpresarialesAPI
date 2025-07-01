using GestorPedidosEmpresarialesBackend.Data;
using GestorPedidosEmpresarialesBackend.Domain;
using System.Collections.Generic;

namespace GestorPedidosEmpresarialesBackend.Business
{
    public class EmpleadoBusiness
    {
        private readonly EmpleadoData _empleadoData;

        public EmpleadoBusiness(EmpleadoData empleadoData)
        {
            _empleadoData = empleadoData;
        }

        public List<Empleado> GetAll() => _empleadoData.GetAll();
        public Empleado GetById(int id) => _empleadoData.GetById(id);
        public Empleado Create(Empleado empleado) => _empleadoData.Create(empleado);
        public void Update(Empleado empleado) => _empleadoData.Update(empleado);
        public void Delete(int id) => _empleadoData.Delete(id);
    }
}