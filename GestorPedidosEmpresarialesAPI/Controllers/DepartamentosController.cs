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

        [HttpPost]
        public IActionResult Create(DepartamentoDto dto)
        {
            var departamento = DepartamentoMapper.ToEntity(dto);
            _departamentoBusiness.Insert(departamento);
            var nuevoDto = DepartamentoMapper.ToDto(departamento);
            return Ok(new { mensaje = "Departamento creado exitosamente" });
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

        [HttpPut("{id}")]
        public IActionResult Update(int id, DepartamentoDto dto)
        {
            if (id != dto.IdDepartamento)
            {
                return BadRequest();
            }
            var departamento = DepartamentoMapper.ToEntity(dto);
            _departamentoBusiness.Update(departamento);
            return Ok(new { mensaje = "Departamento actualizado correctamente" });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existente = _departamentoBusiness.GetById(id);
            if (existente == null)
            {
                return NotFound(new { mensaje = "Departamento no encontrado" });
            }
            _departamentoBusiness.Delete(id);
            return Ok(new { mensaje = "Departamento eliminado correctamente" });

        }
    }
}