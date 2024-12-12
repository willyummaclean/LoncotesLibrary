using System.ComponentModel.DataAnnotations;

namespace Library.Models;

public class MaterialTypeDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DaysCheckedOut { get; set; }

}