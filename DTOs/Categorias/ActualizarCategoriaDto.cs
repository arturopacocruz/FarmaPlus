using System.ComponentModel.DataAnnotations;

namespace FarmaPlus.Dtos.Categorias;

public class ActualizarCategoriaDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [MaxLength(250)]
    public string Descripcion { get; set; } = string.Empty;
}
