using System.ComponentModel.DataAnnotations;

namespace EFCoreDatabaseFirst.Dtos;

public class AssignClientToTripDto
{
    [Required]
    public required string FirstName { get; set; }
    [Required]
    public required string LastName { get; set; }
    [Required, EmailAddress]
    public required string Email { get; set; }
    [Required]
    public required string Telephone { get; set; }
    [Required]
    [StringLength(11, ErrorMessage = "Pesel must be exactly 11 characters.", MinimumLength = 11)]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "Pesel must contain only digits.")]
    public required string Pesel { get; set; }
    [DataType(DataType.Date)]
    public DateTime? PaymentDate { get; set; }
}