using FarmaPlus.Dtos.Categorias;

namespace FarmaPlus.Services;

public interface ICategoriaService
{
    Task<IEnumerable<CategoriaDto>> ObtenerTodasAsync();
    Task<CategoriaDto?> ObtenerPorIdAsync(int id);
    Task<CategoriaDto> CrearAsync(CrearCategoriaDto dto);
    Task<bool> ActualizarAsync(int id, ActualizarCategoriaDto dto);
    Task<bool> EliminarAsync(int id);
}
