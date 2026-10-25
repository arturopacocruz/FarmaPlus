using FarmaPlus.Core;
using FarmaPlus.Dtos.Categorias;
using FarmaPlus.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmaPlus.Services;

public class CategoriaService : ICategoriaService
{
    private readonly FarmaPlusDbContext _context;

    public CategoriaService(FarmaPlusDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoriaDto>> ObtenerTodasAsync()
    {
        return await _context.Categorias
            .Include(c => c.Productos)
            .Select(c => new CategoriaDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                CantidadProductos = c.Productos.Count
            })
            .ToListAsync();
    }

    public async Task<CategoriaDto?> ObtenerPorIdAsync(int id)
    {
        var categoria = await _context.Categorias
            .Include(c => c.Productos)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (categoria == null) return null;

        return new CategoriaDto
        {
            Id = categoria.Id,
            Nombre = categoria.Nombre,
            Descripcion = categoria.Descripcion,
            CantidadProductos = categoria.Productos.Count
        };
    }

    public async Task<CategoriaDto> CrearAsync(CrearCategoriaDto dto)
    {
        var nuevaCategoria = new Categoria
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion
        };

        _context.Categorias.Add(nuevaCategoria);
        await _context.SaveChangesAsync();

        return new CategoriaDto
        {
            Id = nuevaCategoria.Id,
            Nombre = nuevaCategoria.Nombre,
            Descripcion = nuevaCategoria.Descripcion,
            CantidadProductos = 0
        };
    }

    public async Task<bool> ActualizarAsync(int id, ActualizarCategoriaDto dto)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null) return false;

        categoria.Nombre = dto.Nombre;
        categoria.Descripcion = dto.Descripcion;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var categoria = await _context.Categorias.FindAsync(id);
        if (categoria == null) return false;

        _context.Categorias.Remove(categoria);
        await _context.SaveChangesAsync();
        return true;
    }
}
