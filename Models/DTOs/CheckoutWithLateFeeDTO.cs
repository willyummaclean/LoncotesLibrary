using System.ComponentModel.DataAnnotations;

namespace Library.Models;

public class CheckoutWithLateFeeDTO
{
    public int Id { get; set; }
    public int MaterialId { get; set; }
    public MaterialDTO Material { get; set; }
    public int PatronId { get; set; }
    public PatronDTO Patron { get; set; }
    public DateTime CheckedOutSince { get; set; }
    public DateTime? ReturnDate { get; set; }
    private decimal _lateFeePerDay = .50M;
    public decimal? LateFee
    {
        get
        {
            DateTime dueDate = CheckedOutSince.AddDays(Material.MaterialType.DaysCheckedOut);
            DateTime returnDate = ReturnDate ?? DateTime.Today;
            int daysLate = (returnDate - dueDate).Days;
            decimal fee = daysLate * _lateFeePerDay;
            return daysLate > 0 ? fee : null;
        }
    }
}