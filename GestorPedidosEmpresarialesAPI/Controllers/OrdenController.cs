using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesAPI.Mapper;
using GestorPedidosEmpresarialesBackend.Business;
using GestorPedidosEmpresarialesBackend.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorPedidosEmpresarialesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : ControllerBase
    {
        private readonly OrdenBusiness _ordenBusiness;
       

        public OrdenController(OrdenBusiness ordenBusiness, ILogger<OrdenController> logger)
        {
            _ordenBusiness = ordenBusiness ?? throw new ArgumentNullException(nameof(ordenBusiness));
           
        }

        /// <summary>
        /// Retorna todas las órdenes con la información asociada de cliente y empleado.
        /// </summary>
        [HttpGet]
        public ActionResult<List<Orden>> ObtenerOrdenes()
        {
            try
            {
                var ordenes = _ordenBusiness.ObtenerOrdenes();
                return Ok(ordenes);
            }
            catch (ApplicationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado al consultar las órdenes.");
            }
        }

        /// <summary>
        /// Inserta una nueva orden con todos sus detalles.
        /// </summary>
        [HttpPost]
        public ActionResult<int> InsertarOrdenConDetalles([FromBody] OrdenDto ordenDto)
        {
            if (ordenDto == null)
                return BadRequest("La orden no puede ser nula.");
            if (ordenDto.Cliente == null || ordenDto.Empleado == null)
                return BadRequest("La orden debe incluir cliente y empleado.");
            if (ordenDto.DetallesOrden == null || ordenDto.DetallesOrden.Count == 0)
                return BadRequest("La orden debe incluir al menos un detalle.");

            try
            {


                int idOrden = _ordenBusiness.InsertarOrdenConDetalles(OrdenMapper.ToEntity(ordenDto));
                return CreatedAtAction(nameof(ObtenerOrdenes), new { id = idOrden }, idOrden);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error inesperado al registrar la orden.");
            }
        }
    }
}