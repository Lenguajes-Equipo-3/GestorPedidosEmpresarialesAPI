using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesAPI.Mapper;
using GestorPedidosEmpresarialesBackend.Business;
using Microsoft.AspNetCore.Mvc;

namespace GestorPedidosEmpresarialesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParametrosSistemaController : ControllerBase
    {
        private readonly ParametrosSistemaBusiness business;

        public ParametrosSistemaController(ParametrosSistemaBusiness business)
        {
            this.business = business;
        }

        [HttpPost]
        public IActionResult AddParametro([FromBody] ParametrosSistemaDto dto)
        {
            var existe = business.GetParametros();
            if (existe != null)
                return Conflict(new { mensaje = "Los parámetros ya existen. Use PUT para actualizarlos." });

            business.AddParametro(ParametrosSistemaMapper.ToEntity(dto));
            return Ok(new { mensaje = "Parámetros creados exitosamente." });
        }

        [HttpGet]
        public IActionResult GetParametros()
        {

            var parametros = business.GetParametros();
            if (parametros == null)
                return NotFound(new { mensaje = "No se han configurado los parámetros del sistema" });

            return Ok(ParametrosSistemaMapper.ToDto(parametros));
        }

        [HttpPut()]
        public IActionResult UpdateParametros( [FromBody] ParametrosSistemaDto dto)
        {
            var actual = business.GetParametros();
            if (actual == null)
                return NotFound(new { mensaje = "No hay parámetros para actualizar" });

            dto.IdParametro = actual.IdParametro; 
            business.UpdateParametros(ParametrosSistemaMapper.ToEntity(dto));
            return Ok(new { mensaje = "Parámetros actualizados exitosamente." });
        }
    }
}
