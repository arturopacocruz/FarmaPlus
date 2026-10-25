using FarmaPlus.Core;
using FarmaPlus.Dtos.Productos;
using FarmaPlus.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmaPlus.Services;

public class ProductoService : IProductoService
{
    private readonly FarmaPlusDbContext _context;

    public ProductoService(FarmaPlusDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductoDto>> ObtenerTodosAsync(int? categoriaId = null, string? buscar = null)
    {
        var query = _context.Productos.Include(p => p.Categoria).AsQueryable();

        if (categoriaId.HasValue)
        {
            query = query.Where(p => p.CategoriaId == categoriaId.Value);
        }

        if (!string.IsNullOrWhiteSpace(buscar))
        {
            query = query.Where(p => p.Nombre.Contains(buscar) || p.Descripcion.Contains(buscar) || p.CodigoBarras.Contains(buscar));
        }

        return await query.Select(p => new ProductoDto
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion,
            Precio = p.Precio,
            Stock = p.Stock,
            CodigoBarras = p.CodigoBarras,
            RequiereReceta = p.RequiereReceta,
            FechaVencimiento = p.FechaVencimiento,
            CategoriaId = p.CategoriaId,
            CategoriaNombre = p.Categoria != null ? p.Categoria.Nombre : string.Empty
        }).ToListAsync();
    }

    public async Task<ProductoDto?> ObtenerPorIdAsync(int id)
    {
        var producto = await _context.Productos
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (producto == null) return null;

        return new ProductoDto
        {
            Id = producto.Id,
            Nombre = producto.Nombre,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio,
            Stock = producto.Stock,
            CodigoBarras = producto.CodigoBarras,
            RequiereReceta = producto.RequiereReceta,
            FechaVencimiento = producto.FechaVencimiento,
            CategoriaId = producto.CategoriaId,
            CategoriaNombre = producto.Categoria != null ? producto.Categoria.Nombre : string.Empty
        };
    }

    public async Task<ProductoDto?> CrearAsync(CrearProductoDto dto)
    {
        var existeCategoria = await _context.Categorias.AnyAsync(c => c.Id == dto.CategoriaId);
        if (!existeCategoria) return null;

        var nuevoProducto = new Producto
        {
            Nombre = dto.Nombre,
            Descripcion = dto.Descripcion,
            Precio = dto.Precio,
            Stock = dto.Stock,
            CodigoBarras = dto.CodigoBarras,
            RequiereReceta = dto.RequiereReceta,
            FechaVencimiento = dto.FechaVencimiento,
            CategoriaId = dto.CategoriaId
        };

        _context.Productos.Add(nuevoProducto);
        await _context.SaveChangesAsync();

        await _context.Entry(nuevoProducto).Reference(p => p.Categoria).LoadAsync();

        return new ProductoDto
        {
            Id = nuevoProducto.Id,
            Nombre = nuevoProducto.Nombre,
            Descripcion = nuevoProducto.Descripcion,
            Precio = nuevoProducto.Precio,
            Stock = nuevoProducto.Stock,
            CodigoBarras = nuevoProducto.CodigoBarras,
            RequiereReceta = nuevoProducto.RequiereReceta,
            FechaVencimiento = nuevoProducto.FechaVencimiento,
            CategoriaId = nuevoProducto.CategoriaId,
            CategoriaNombre = nuevoProducto.Categoria != null ? nuevoProducto.Categoria.Nombre : string.Empty
        };
    }

    public async Task<bool> ActualizarAsync(int id, ActualizarProductoDto dto)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null) return false;

        var existeCategoria = await _context.Categorias.AnyAsync(c => c.Id == dto.CategoriaId);
        if (!existeCategoria) return false;

        producto.Nombre = dto.Nombre;
        producto.Descripcion = dto.Descripcion;
        producto.Precio = dto.Precio;
        producto.Stock = dto.Stock;
        producto.CodigoBarras = dto.CodigoBarras;
        producto.RequiereReceta = dto.RequiereReceta;
        producto.FechaVencimiento = dto.FechaVencimiento;
        producto.CategoriaId = dto.CategoriaId;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EliminarAsync(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null) return false;

        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();
        return true;
    }
}
