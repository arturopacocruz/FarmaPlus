using FarmaPlus.Dtos.Productos;

namespace FarmaPlus.Services;

public interface IProductoService
{
    Task<IEnumerable<ProductoDto>> ObtenerTodosAsync(int? categoriaId = null, string? buscar = null);
    Task<ProductoDto?> ObtenerPorIdAsync(int id);
    Task<ProductoDto?> CrearAsync(CrearProductoDto dto);
    Task<bool> ActualizarAsync(int id, ActualizarProductoDto dto);
    Task<bool> EliminarAsync(int id);
}
