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
    }
}