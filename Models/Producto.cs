using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmaPlus.Models;

public class Producto
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(150, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    [MaxLength(300)]
    public string Descripcion { get; set; } = string.Empty;

    [Range(0.01, 100000.0)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Precio { get; set; }

    [Range(0, 100000)]
    public int Stock { get; set; }

    [MaxLength(50)]
    public string CodigoBarras { get; set; } = string.Empty;

    public bool RequiereReceta { get; set; }

    public DateTime FechaVencimiento { get; set; }

    [Required]
    [ForeignKey("Categoria")]
    public int CategoriaId { get; set; }

    public Categoria? Categoria { get; set; }
}
