using GestorPedidosEmpresarialesBackend.Data;
using GestorPedidosEmpresarialesBackend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Business
{
    public class ProductoBaseBusiness
    {
        private readonly ProductoBaseData productoBaseData;

        public ProductoBaseBusiness(ProductoBaseData productoBaseData)
        {
            this.productoBaseData = productoBaseData;
        }

        public void AgregarProductoBase(ProductoBase productoBase)
        {
            if (productoBase == null)
            {
                throw new ArgumentNullException(nameof(productoBase), "El producto base no puede ser nulo.");
            }
            try
            {
                productoBaseData.Insert(productoBase);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el producto base.", ex);
            }
        }


        public List<ProductoBase> ObtenerProductosBase()
        {
            try
            {
                return productoBaseData.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los productos base.", ex);
            }
        }

        public ProductoBase ObtenerProductoBasePorId(int idProductoBase)
        {
            try
            {
                return productoBaseData.GetById(idProductoBase);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el producto base con ID {idProductoBase}.", ex);
            }
        }

        public void ActualizarProductoBase(ProductoBase productoBase)
        {
            if (productoBase == null)
            {
                throw new ArgumentNullException(nameof(productoBase), "El producto base no puede ser nulo.");
            }
            try
            {
                productoBaseData.Update(productoBase);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el producto base.", ex);
            }
        }

        public void EliminarProductoBase(int idProductoBase)
        {
            try
            {
                productoBaseData.Delete(idProductoBase);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar el producto base con ID {idProductoBase}.", ex);
            }
        }
    }
}
