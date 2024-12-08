using System.ComponentModel.DataAnnotations;

namespace Library.Models;

public class Checkout
{
    public int Id { get; set; }
    [Required]
    public int MaterialId { get; set; }
    [Required]
    public int PatronId { get; set; }
    [Required]
    public DateTime CheckedOutSince { get; set; }
    public DateTime? ReturnDate { get; set; }
}