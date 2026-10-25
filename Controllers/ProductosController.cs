using FarmaPlus.Dtos.Productos;
using FarmaPlus.Services;
using Microsoft.AspNetCore.Mvc;

namespace FarmaPlus.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _productoService;

    public ProductosController(IProductoService productoService)
    {
        _productoService = productoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductoDto>>> ObtenerTodos([FromQuery] int? categoriaId, [FromQuery] string? buscar)
    {
        var productos = await _productoService.ObtenerTodosAsync(categoriaId, buscar);
        return Ok(productos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductoDto>> ObtenerPorId(int id)
    {
        var producto = await _productoService.ObtenerPorIdAsync(id);
        if (producto == null) return NotFound(new { mensaje = $"No se encontró el producto con ID {id}" });

        return Ok(producto);
    }

    [HttpPost]
    public async Task<ActionResult<ProductoDto>> Crear([FromBody] CrearProductoDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var nuevoProducto = await _productoService.CrearAsync(dto);
        if (nuevoProducto == null)
        {
            return BadRequest(new { mensaje = $"La categoría con ID {dto.CategoriaId} no existe" });
        }

        return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevoProducto.Id }, nuevoProducto);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarProductoDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var actualizado = await _productoService.ActualizarAsync(id, dto);
        if (!actualizado)
        {
            return NotFound(new { mensaje = $"No se pudo actualizar el producto con ID {id}. Verifique que exista y que la categoría sea válida." });
        }

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var eliminado = await _productoService.EliminarAsync(id);
        if (!eliminado) return NotFound(new { mensaje = $"No se encontró el producto con ID {id}" });

        return NoContent();
    }
}
