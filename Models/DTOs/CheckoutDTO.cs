using System.ComponentModel.DataAnnotations;

namespace Library.Models;

public class CheckoutDTP
{
    public int Id { get; set; }
    [Required]
    public Material MaterialDTO { get; set; }
    [Required]
    public Patron PatronDTO { get; set; }
    [Required]
    public DateTime CheckedOutSince { get; set; }
    public DateTime? ReturnDate { get; set; }
}