using System.ComponentModel.DataAnnotations;

namespace Library.Models;

public class MaterialDTO
{
    public int Id { get; set; }
    [Required]
    public string MaterialName { get; set; }
    [Required]
    public MaterialType MaterialTypeDTO { get; set; }
    [Required]
    public Genre GenreDTO { get; set; }
    public DateTime? OutOfCirculationSince { get; set; }
}