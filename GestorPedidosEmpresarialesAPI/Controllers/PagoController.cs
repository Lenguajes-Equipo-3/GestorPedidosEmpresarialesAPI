using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesAPI.Mapper;
using GestorPedidosEmpresarialesBackend.Business;
using Microsoft.AspNetCore.Mvc;

namespace GestorPedidosEmpresarialesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagoController : ControllerBase
    {
        private readonly PagoBusiness pagobusiness;

        public PagoController(PagoBusiness pagobusiness)
        {
            this.pagobusiness = pagobusiness;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var lista = pagobusiness.GetAll().Select(PagoMapper.ToDto);
            return Ok(lista);
        }

        [HttpPost]
        public IActionResult Insert([FromBody] PagoDto dto)
        {
            var entity = PagoMapper.ToEntity(dto);
            pagobusiness.Insert(entity);
            return Ok(new { mensaje = "Pago registrado correctamente" });
        }


    }
}
