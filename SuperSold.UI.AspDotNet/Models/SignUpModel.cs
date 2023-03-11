using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperSold.UI.AspDotNet.Models;

public class SignUpModel {

    [Required(AllowEmptyStrings = false, ErrorMessage = "Must write an username.")]
    [DataType(DataType.Text)]
    public required string UserName { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Must insert an email.")]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Must write a password.")]
    [StringLength(int.MaxValue, MinimumLength = 8, ErrorMessage = "The password must be at least 8 characters.")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Must confirm your password.")]
    [Compare(nameof(Password), ErrorMessage = "The two passwords must match.")]
    [NotMapped]
    [DataType(DataType.Password)]
    public required string RepeatPassword { get; set; }

    //todo - implement custom "must be true" attribute
    [Required]
    //[Range(typeof(bool), "true", "true", ErrorMessage = "Must agree to the terms to continue.")]
    public bool AcceptTerms { get; set; }

    public bool RememberMe { get; set; }

}