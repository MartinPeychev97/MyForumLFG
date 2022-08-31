using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Services.Requests
{
    public class LoginUserRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;

        [JsonIgnore]
        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
