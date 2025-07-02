using GestorPedidosEmpresarialesBackend.Data;
using GestorPedidosEmpresarialesBackend.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorPedidosEmpresarialesBackend.Business
{
    public class OrdenBusiness
    {
        private readonly OrdenData _ordenData;
 

        // Inyección de dependencias
        public OrdenBusiness(OrdenData ordenData, ILogger<OrdenBusiness> logger = null)
        {
            _ordenData = ordenData ?? throw new ArgumentNullException(nameof(ordenData));
         
        }

        /// <summary>
        /// Inserta una orden y todos sus detalles de forma transaccional.
        /// </summary>
        /// <param name="orden">Orden con todos los datos y detalles.</param>
        /// <returns>ID de la orden generada.</returns>
        public int InsertarOrdenConDetalles(Orden orden)
        {
            if (orden == null)
                throw new ArgumentNullException(nameof(orden), "La orden no puede ser nula.");
            if (orden.Cliente == null)
                throw new ArgumentException("La orden debe tener un cliente.");
            if (orden.Empleado == null)
                throw new ArgumentException("La orden debe tener un empleado.");
            if (orden.DetallesOrden == null || orden.DetallesOrden.Count == 0)
                throw new ArgumentException("La orden debe tener al menos un detalle.");

            try
            {
                return _ordenData.InsertarOrdenConDetalles(orden);
            }
            catch (Exception ex)
            {
                // Si el mensaje es de inventario insuficiente, lo propaga limpio
                if (ex.Message.Contains("No hay suficiente inventario"))
                {
                    throw new ApplicationException(ex.Message );
                }
                // Puede agregar aquí otros mensajes personalizados para otros casos
                throw new ApplicationException("Ocurrió un error al registrar la orden. Por favor, intente nuevamente o contacte a soporte.", ex);
            }
        }


        /// <summary>
        /// Obtiene todas las órdenes con información de cliente , empleado y detalles correspondites.
        /// </summary>
        /// <returns>Lista de órdenes.</returns>
        public List<Orden> ObtenerOrdenes()
        {
            try
            {
                return _ordenData.ObtenerOrdenes();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error al consultar las órdenes.", ex);
            }
        }
    }
}