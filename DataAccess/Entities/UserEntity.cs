using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities;

public class UserEntity : IdentityUser
{
    [StringLength(40, MinimumLength = 3)]
    public string FirstName { get; set; } = String.Empty;
    [StringLength(40, MinimumLength = 3)]
    public string Lastname { get; set; } = String.Empty;
}

