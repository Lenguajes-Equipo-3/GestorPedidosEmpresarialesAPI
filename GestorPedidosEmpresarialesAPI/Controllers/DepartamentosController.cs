using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesAPI.Mapper;
using GestorPedidosEmpresarialesBackend.Business;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GestorPedidosEmpresarialesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartamentosController : ControllerBase
    {
        private readonly DepartamentoBusiness _departamentoBusiness;

        public DepartamentosController(DepartamentoBusiness departamentoBusiness)
        {
            _departamentoBusiness = departamentoBusiness;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var departamentos = _departamentoBusiness.GetAll();
            var dtos = departamentos.Select(DepartamentoMapper.ToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var departamento = _departamentoBusiness.GetById(id);
            if (departamento == null)
            {
                return NotFound();
            }
            var dto = DepartamentoMapper.ToDto(departamento);
            return Ok(dto);
        }
    }
}