using System.ComponentModel.DataAnnotations;

namespace Services.Models.Responses;

public class UserResponse
{
    [StringLength(40, MinimumLength = 3)]
    public string FirstName { get; set; } = String.Empty;
    [StringLength(40, MinimumLength = 3)]
    public string Lastname { get; set; } = String.Empty;
    [EmailAddress]
    public string Email { get; set; } = String.Empty;
}
