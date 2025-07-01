using GestorPedidosEmpresarialesAPI.DTOs;
using GestorPedidosEmpresarialesAPI.Mapper;
using GestorPedidosEmpresarialesBackend.Business;
using Microsoft.AspNetCore.Mvc;

namespace GestorPedidosEmpresarialesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaBusiness categoriaBusiness;

        public CategoriaController(CategoriaBusiness categoriaBusiness)
        {
            this.categoriaBusiness = categoriaBusiness;
        }

        [HttpGet]
        public IActionResult GetAllCategorias()
        {
            var categorias = categoriaBusiness.GetAllCategorias();
            var dtoList = categorias.Select(CategoriaMapper.ToDto);
            return Ok(dtoList);
        }

        [HttpPost]
        public IActionResult AddCategoria([FromBody] CategoriaDto dto)
        {
            var categoria = CategoriaMapper.ToEntity(dto);
            categoriaBusiness.AddCategoria(categoria);
            return Ok(new { mensaje = "Categoría creada exitosamente" });
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoriaById(int id)
        {
            var categoria = categoriaBusiness.GetCategoriaById(id);
            if (categoria == null)
                return NotFound(new { mensaje = "Categoría no encontrada" });

            return Ok(CategoriaMapper.ToDto(categoria));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategoria(int id)
        {
            var existente = categoriaBusiness.GetCategoriaById(id);
            if (existente == null)
                return NotFound(new { mensaje = "Categoría no encontrada" });

            categoriaBusiness.DeleteCategoria(id);
            return Ok(new { mensaje = "Categoría eliminada correctamente" });
        }

    }

}
