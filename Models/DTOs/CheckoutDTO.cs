using System.ComponentModel.DataAnnotations;

namespace Library.Models;

public class CheckoutDTP
{
    public int Id { get; set; }
    public int MaterialId { get; set; }
    public MaterialDTO Material { get; set; }
    public int PatronId { get; set; }
    public PatronDTO Patron { get; set; }
    public DateTime CheckedOutSince { get; set; }
    public DateTime? ReturnDate { get; set; }
}