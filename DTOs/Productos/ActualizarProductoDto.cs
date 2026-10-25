using System.ComponentModel.DataAnnotations;

namespace FarmaPlus.Dtos.Productos;

public class ActualizarProductoDto
{
    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string Descripcion { get; set; } = string.Empty;

    [Range(0.01, 100000.0)]
    public decimal Precio { get; set; }

    [Range(0, 100000)]
    public int Stock { get; set; }

    [MaxLength(50)]
    public string CodigoBarras { get; set; } = string.Empty;

    public bool RequiereReceta { get; set; }

    [Required]
    public DateTime FechaVencimiento { get; set; }

    [Range(1, int.MaxValue)]
    public int CategoriaId { get; set; }
}
