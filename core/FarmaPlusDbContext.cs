using FarmaPlus.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmaPlus.Core;

public class FarmaPlusDbContext : DbContext
{
    public FarmaPlusDbContext(DbContextOptions<FarmaPlusDbContext> options) : base(options)
    {
    }

    public DbSet<Producto> Productos => Set<Producto>();
    public DbSet<Categoria> Categorias => Set<Categoria>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Producto>()
            .Property(p => p.FechaVencimiento)
            .HasColumnType("timestamp without time zone");

        modelBuilder.Entity<Categoria>().HasData(
            new Categoria
            {
                Id = 1,
                Nombre = "Medicamentos",
                Descripcion = "Fármacos y tratamientos terapéuticos"
            },
            new Categoria
            {
                Id = 2,
                Nombre = "Cuidado Personal",
                Descripcion = "Higiene y cuidado corporal"
            },
            new Categoria
            {
                Id = 3,
                Nombre = "Vitaminas y Suplementos",
                Descripcion = "Nutrición y complementos vitamínicos"
            }
        );

        modelBuilder.Entity<Producto>().HasData(
            new Producto
            {
                Id = 1,
                Nombre = "Paracetamol 500mg",
                Descripcion = "Analgésico y antipirético en tabletas",
                Precio = 3.50m,
                Stock = 120,
                CodigoBarras = "7750123456789",
                RequiereReceta = false,
                FechaVencimiento = new DateTime(2027, 6, 30),
                CategoriaId = 1
            },
            new Producto
            {
                Id = 2,
                Nombre = "Amoxicilina 500mg",
                Descripcion = "Antibiótico de amplio espectro en cápsulas",
                Precio = 12.00m,
                Stock = 50,
                CodigoBarras = "7750987654321",
                RequiereReceta = true,
                FechaVencimiento = new DateTime(2026, 12, 15),
                CategoriaId = 1
            },
            new Producto
            {
                Id = 3,
                Nombre = "Alcohol en Gel 500ml",
                Descripcion = "Antiséptico antibacterial para manos",
                Precio = 7.50m,
                Stock = 80,
                CodigoBarras = "7751122334455",
                RequiereReceta = false,
                FechaVencimiento = new DateTime(2028, 1, 1),
                CategoriaId = 2
            },
            new Producto
            {
                Id = 4,
                Nombre = "Vitamina C + Zinc 1000mg",
                Descripcion = "Tabletas efervescentes para reforzar defensas",
                Precio = 15.00m,
                Stock = 65,
                CodigoBarras = "7755566778899",
                RequiereReceta = false,
                FechaVencimiento = new DateTime(2027, 9, 20),
                CategoriaId = 3
            }
        );
    }
}
