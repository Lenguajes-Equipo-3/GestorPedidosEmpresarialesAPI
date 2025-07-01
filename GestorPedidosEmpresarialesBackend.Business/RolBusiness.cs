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
    }
}