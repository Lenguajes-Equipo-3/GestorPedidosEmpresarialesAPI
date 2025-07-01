using GestorPedidosEmpresarialesBackend.Data;
using GestorPedidosEmpresarialesBackend.Domain;
using System;
using System.Collections.Generic;

namespace GestorPedidosEmpresarialesBackend.Business
{
    public class ProductoVarianteBusiness
    {
        private readonly ProductoVarianteData productoVarianteData;

        public ProductoVarianteBusiness(ProductoVarianteData productoVarianteData)
        {
            this.productoVarianteData = productoVarianteData;
        }

        public List<VarianteProducto> ObtenerVariantesProducto()
        {
            try
            {
                return productoVarianteData.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las variantes de producto.", ex);
            }
        }

        public VarianteProducto ObtenerVarianteProductoPorId(int id)
        {
            try
            {
                return productoVarianteData.GetById(id) ?? throw new Exception("Variante de producto no encontrada.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la variante de producto con ID {id}.", ex);
            }
        }

        public void AgregarVarianteProducto(VarianteProducto varianteProducto)
        {
            if (varianteProducto == null)
                throw new ArgumentNullException(nameof(varianteProducto), "La variante de producto no puede ser nula.");

            try
            {
                productoVarianteData.Insert(varianteProducto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la variante de producto.", ex);
            }
        }

        public void ActualizarVarianteProducto(VarianteProducto varianteProducto)
        {
            if (varianteProducto == null)
                throw new ArgumentNullException(nameof(varianteProducto), "La variante de producto no puede ser nula.");

            try
            {
                productoVarianteData.Update(varianteProducto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la variante de producto.", ex);
            }
        }

        public void EliminarVarianteProducto(int id)
        {
            try
            {
                productoVarianteData.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la variante de producto con ID {id}.", ex);
            }
        }
    }
}