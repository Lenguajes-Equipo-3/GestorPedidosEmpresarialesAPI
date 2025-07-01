using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesAPI.Mapper;
using GestorPedidosEmpresarialesBackend.Business;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GestorPedidosEmpresarialesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpleadosController : ControllerBase
    {
        private readonly EmpleadoBusiness _empleadoBusiness;

        public EmpleadosController(EmpleadoBusiness empleadoBusiness)
        {
            _empleadoBusiness = empleadoBusiness;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var empleados = _empleadoBusiness.GetAll();
            var dtos = empleados.Select(EmpleadoMapper.ToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var empleado = _empleadoBusiness.GetById(id);
            if (empleado == null)
            {
                return NotFound();
            }
            var dto = EmpleadoMapper.ToDto(empleado);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create(EmpleadoDto dto)
        {
            var empleado = EmpleadoMapper.ToEntity(dto);
            var nuevoEmpleado = _empleadoBusiness.Create(empleado);
            var nuevoDto = EmpleadoMapper.ToDto(nuevoEmpleado);
            return CreatedAtAction(nameof(GetById), new { id = nuevoDto.IdEmpleado }, nuevoDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, EmpleadoDto dto)
        {
            if (id != dto.IdEmpleado)
            {
                return BadRequest();
            }
            var empleado = EmpleadoMapper.ToEntity(dto);
            _empleadoBusiness.Update(empleado);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _empleadoBusiness.Delete(id);
            return NoContent();
        }
    }
}