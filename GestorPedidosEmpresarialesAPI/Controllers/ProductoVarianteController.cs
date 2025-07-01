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
    public class ProductoVarianteController : ControllerBase
    {
        private readonly ProductoVarianteBusiness productoVarianteBusiness;

        public ProductoVarianteController(ProductoVarianteBusiness productoVarianteBusiness)
        {
            this.productoVarianteBusiness = productoVarianteBusiness;
        }

        // GET: api/ProductoVariante
        [HttpGet]
        public IActionResult GetAllVariantes()
        {
            try
            {
                var variantes = productoVarianteBusiness.ObtenerVariantesProducto();
                var dtoList = variantes.Select(ProductoVarianteMapper.ToDto);
                return Ok(dtoList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }

        // GET: api/ProductoVariante/{id}
        [HttpGet("{id}")]
        public IActionResult GetVarianteById(int id)
        {
            try
            {
                var variante = productoVarianteBusiness.ObtenerVarianteProductoPorId(id);
                if (variante == null)
                    return NotFound(new { mensaje = "Variante de producto no encontrada" });

                return Ok(ProductoVarianteMapper.ToDto(variante));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }

        // POST: api/ProductoVariante
        [HttpPost]
        public IActionResult AddVariante([FromBody] ProductoVarianteDto dto)
        {
            try
            {
                var variante = ProductoVarianteMapper.ToEntity(dto);
                productoVarianteBusiness.AgregarVarianteProducto(variante);
                return Ok(new { mensaje = "Variante de producto creada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }

        // PUT: api/ProductoVariante/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateVariante(int id, [FromBody] ProductoVarianteDto dto)
        {
            try
            {
                if (id != dto.IdVariante)
                    return BadRequest(new { mensaje = "El ID de la variante no coincide" });

                var existente = productoVarianteBusiness.ObtenerVarianteProductoPorId(id);
                if (existente == null)
                    return NotFound(new { mensaje = "Variante de producto no encontrada" });

                var actualizado = ProductoVarianteMapper.ToEntity(dto);
                productoVarianteBusiness.ActualizarVarianteProducto(actualizado);
                return Ok(new { mensaje = "Variante de producto actualizada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }

        // DELETE: api/ProductoVariante/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteVariante(int id)
        {
            try
            {
                var existente = productoVarianteBusiness.ObtenerVarianteProductoPorId(id);
                if (existente == null)
                    return NotFound(new { mensaje = "Variante de producto no encontrada" });

                productoVarianteBusiness.EliminarVarianteProducto(id);
                return Ok(new { mensaje = "Variante de producto eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno del servidor", detalle = ex.Message });
            }
        }
    }
}