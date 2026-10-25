using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FarmaPlus.Models;

public class Categoria
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Nombre { get; set; } = string.Empty;

    [MaxLength(250)]
    public string Descripcion { get; set; } = string.Empty;

    [JsonIgnore]
    public ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
