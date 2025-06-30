using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesAPI.Mapper;
using GestorPedidosEmpresarialesBackend.Business;
using Microsoft.AspNetCore.Mvc;

namespace GestorPedidosEmpresarialesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteBusiness clienteBusiness;

        public ClientesController(ClienteBusiness clienteBusiness)
        {
            this.clienteBusiness = clienteBusiness;
        }

        [HttpGet]
        public IActionResult GetAllClientes()
        {
            var clientes = clienteBusiness.GetAllClientes();
            var dtoList = clientes.Select(ClienteMapper.ToDto);
            return Ok(dtoList);
        }

        [HttpPost]
        public IActionResult AddCliente([FromBody] ClienteDto dto)
        {
            var cliente = ClienteMapper.ToEntity(dto);
            clienteBusiness.AddCliente(cliente);
            return Ok(new { mensaje = "Cliente creado exitosamente" });
        }


        [HttpGet("{id}")]
        public IActionResult GetClienteById(int id)
        {
            var cliente = clienteBusiness.GetClienteById(id);
            if (cliente == null)
                return NotFound(new { mensaje = "Cliente no encontrado" });

            return Ok(ClienteMapper.ToDto(cliente));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCliente(int id, [FromBody] ClienteDto dto)
        {
            if (id != dto.IdCliente)
                return BadRequest(new { mensaje = "El ID del cliente no coincide" });

            var existente = clienteBusiness.GetClienteById(id);
            if (existente == null)
                return NotFound(new { mensaje = "Cliente no encontrado" });

            var actualizado = ClienteMapper.ToEntity(dto);
            clienteBusiness.UpdateCliente(actualizado);
            return Ok(new { mensaje = "Cliente actualizado correctamente" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCliente(int id)
        {
            var existente = clienteBusiness.GetClienteById(id);
            if (existente == null)
                return NotFound(new { mensaje = "Cliente no encontrado" });

            clienteBusiness.DeleteCliente(id);
            return Ok(new { mensaje = "Cliente eliminado correctamente" });
        }

    }
}
