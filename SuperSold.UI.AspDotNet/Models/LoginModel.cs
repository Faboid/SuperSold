using System.ComponentModel.DataAnnotations;

namespace SuperSold.UI.AspDotNet.Models;

public class LoginModel {

    [Required(AllowEmptyStrings = false, ErrorMessage = "Must write an username.")]
    [DataType(DataType.Text)]
    public required string UserName { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Must write a password.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "The password must have at least 8 characters of length.")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    public bool RememberMe { get; set; }

}
