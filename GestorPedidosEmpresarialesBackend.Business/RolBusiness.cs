using GestorPedidosEmpresarialesBackend.Data;
using GestorPedidosEmpresarialesBackend.Domain;
using System.Collections.Generic;

namespace GestorPedidosEmpresarialesBackend.Business
{
    public class RolBusiness
    {
        private readonly RolData _rolData;

        public RolBusiness(RolData rolData)
        {
            _rolData = rolData;
        }

        public List<Rol> GetAll() => _rolData.GetAll();
        public Rol GetById(int id) => _rolData.GetById(id);
        public Rol Create(Rol rol) => _rolData.Create(rol);
        public void Update(Rol rol) => _rolData.Update(rol);
        public void Delete(int id) => _rolData.Delete(id);
    }
}