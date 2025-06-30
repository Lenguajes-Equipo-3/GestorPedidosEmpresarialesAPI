using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesAPI.Mapper;
using GestorPedidosEmpresarialesBackend.Business;
using System;
using System.Linq;

namespace GestorPedidosEmpresarialesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoBaseController : ControllerBase
    {
        private readonly ProductoBaseBusiness productoBaseBusiness;

        public ProductoBaseController(ProductoBaseBusiness productoBaseBusiness)
        {
            this.productoBaseBusiness = productoBaseBusiness;
        }

        // GET: api/ProductoBase
        [HttpGet]
        public IActionResult GetAllProductosBase()
        {
            try
            {
                var productos = productoBaseBusiness.ObtenerProductosBase();
                var dtoList = productos.Select(ProductoBaseMapper.ToDto);
                return Ok(dtoList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }

        // GET: api/ProductoBase/{id}
        [HttpGet("{id}")]
        public IActionResult GetProductoBaseById(int id)
        {
            try
            {
                var producto = productoBaseBusiness.ObtenerProductoBasePorId(id);
                if (producto == null)
                    return NotFound(new { mensaje = "Producto base no encontrado" });

                return Ok(ProductoBaseMapper.ToDto(producto));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }

        // POST: api/ProductoBase
        [HttpPost]
        public IActionResult AddProductoBase([FromBody] ProductoBaseDto dto)
        {
            try
            {
                var producto = ProductoBaseMapper.ToEntity(dto);
                productoBaseBusiness.AgregarProductoBase(producto);
                return Ok(new { mensaje = "Producto base creado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }

        // PUT: api/ProductoBase/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateProductoBase(int id, [FromBody] ProductoBaseDto dto)
        {
            try
            {
                if (id != dto.IdProductoBase)
                    return BadRequest(new { mensaje = "El ID del producto base no coincide" });

                var existente = productoBaseBusiness.ObtenerProductoBasePorId(id);
                if (existente == null)
                    return NotFound(new { mensaje = "Producto base no encontrado" });

                var actualizado = ProductoBaseMapper.ToEntity(dto);
                productoBaseBusiness.ActualizarProductoBase(actualizado);
                return Ok(new { mensaje = "Producto base actualizado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }

        // DELETE: api/ProductoBase/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProductoBase(int id)
        {
            try
            {
                var existente = productoBaseBusiness.ObtenerProductoBasePorId(id);
                if (existente == null)
                    return NotFound(new { mensaje = "Producto base no encontrado" });

                productoBaseBusiness.EliminarProductoBase(id);
                return Ok(new { mensaje = "Producto base eliminado correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }
    }
}
