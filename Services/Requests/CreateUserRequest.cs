using System.ComponentModel.DataAnnotations;

namespace Services.Requests
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; } =String.Empty;
        public string LastName { get; set; } =String.Empty;
        public string Email { get; set; } = String.Empty;
        [DataType(DataType.Password)]
        [StringLength(44, ErrorMessage = "Password must not exceed 44 characters")]
        public string Password { get; set; } = String.Empty;
        [StringLength(44, ErrorMessage = "Password must not exceed 44 characters")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password must match.")]
        public string ConfirmPassword { get; set; } = String.Empty;
        [Phone]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Phone number must only contain numbers")]
        public string PhoneNumber { get; set; } = String.Empty;
    }
}
