using FarmaPlus.Dtos.Categorias;
using FarmaPlus.Services;
using Microsoft.AspNetCore.Mvc;

namespace FarmaPlus.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriasController : ControllerBase
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDto>>> ObtenerTodas()
    {
        var categorias = await _categoriaService.ObtenerTodasAsync();
        return Ok(categorias);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CategoriaDto>> ObtenerPorId(int id)
    {
        var categoria = await _categoriaService.ObtenerPorIdAsync(id);
        if (categoria == null) return NotFound(new { mensaje = $"No se encontró la categoría con ID {id}" });

        return Ok(categoria);
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaDto>> Crear([FromBody] CrearCategoriaDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var nuevaCategoria = await _categoriaService.CrearAsync(dto);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = nuevaCategoria.Id }, nuevaCategoria);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarCategoriaDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var actualizado = await _categoriaService.ActualizarAsync(id, dto);
        if (!actualizado) return NotFound(new { mensaje = $"No se encontró la categoría con ID {id}" });

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var eliminado = await _categoriaService.EliminarAsync(id);
        if (!eliminado) return NotFound(new { mensaje = $"No se encontró la categoría con ID {id}" });

        return NoContent();
    }
}
