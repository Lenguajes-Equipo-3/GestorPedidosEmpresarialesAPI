using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesAPI.Mapper;
using GestorPedidosEmpresarialesBackend.Business;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace GestorPedidosEmpresarialesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly RolBusiness _rolBusiness;

        public RolesController(RolBusiness rolBusiness)
        {
            _rolBusiness = rolBusiness;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var roles = _rolBusiness.GetAll();
            var dtos = roles.Select(RolMapper.ToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var rol = _rolBusiness.GetById(id);
            if (rol == null)
            {
                return NotFound();
            }
            var dto = RolMapper.ToDto(rol);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult Create(RolDto dto)
        {
            var rol = RolMapper.ToEntity(dto);
            var nuevoRol = _rolBusiness.Create(rol);
            var nuevoDto = RolMapper.ToDto(nuevoRol);
            return CreatedAtAction(nameof(GetById), new { id = nuevoDto.IdRol }, nuevoDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, RolDto dto)
        {
            if (id != dto.IdRol)
            {
                return BadRequest();
            }
            var rol = RolMapper.ToEntity(dto);
            _rolBusiness.Update(rol);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _rolBusiness.Delete(id);
            return NoContent();
        }
    }
}