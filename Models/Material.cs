using System.ComponentModel.DataAnnotations;

namespace Library.Models;

public class Material
{
    public int Id { get; set; }
    [Required]
    public string MaterialName { get; set; }
    [Required]
    public int MaterialTypeId { get; set; }
    [Required]
    public MaterialType MaterialType { get; set; }
    [Required]
    public int GenreId { get; set; }
    [Required]
    public Genre Genre { get; set; }
    public DateTime? OutOfCirculationSince { get; set; }
    public ICollection<Checkout> Checkouts { get; set; }
}