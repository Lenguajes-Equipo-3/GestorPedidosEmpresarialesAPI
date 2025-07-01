using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesAPI.Mapper;
using GestorPedidosEmpresarialesBackend.Business;
using Microsoft.AspNetCore.Mvc;

namespace GestorPedidosEmpresarialesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MetodoPagoController : ControllerBase
    {
        private readonly MetodoPagoBusiness business;

        public MetodoPagoController(MetodoPagoBusiness business)
        {
            this.business = business;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var lista = business.GetAll().Select(MetodoPagoMapper.ToDto);
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var metodo = business.GetById(id);
            if (metodo == null) return NotFound();
            return Ok(MetodoPagoMapper.ToDto(metodo));
        }
    }
}
