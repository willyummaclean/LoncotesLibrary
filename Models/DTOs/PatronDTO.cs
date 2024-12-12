using System.ComponentModel.DataAnnotations;

namespace Library.Models;

public class PatronDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public bool IsActive { get; set; }
    public string Email { get; set; }

}
