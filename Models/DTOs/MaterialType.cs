using System.ComponentModel.DataAnnotations;

namespace Library.Models;

public class MaterialTypeDTO
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public int DaysCheckedOut { get; set; }

}