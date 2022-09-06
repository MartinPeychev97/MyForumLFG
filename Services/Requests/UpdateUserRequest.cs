using System.ComponentModel.DataAnnotations;

namespace Services.Requests;

public class UpdateUserRequest
{
    public string UserName { get; set; } = String.Empty;
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;

    [EmailAddress]
    public string Email { get; set; } = String.Empty;

    private string _password = String.Empty;
    [MinLength(6)]
    public string Password
    {
        get => _password;
        set => _password = replaceEmptyWithNull(value);
    }

    private string _confirmPassword = String.Empty;
    [Compare("Password")]
    public string ConfirmPassword
    {
        get => _confirmPassword;
        set => _confirmPassword = replaceEmptyWithNull(value);
    }
    // helpers

    private string replaceEmptyWithNull(string value)
    {
        // replace empty string with null to make field optional
        return string.IsNullOrEmpty(value) ? null : value;
    }
}
