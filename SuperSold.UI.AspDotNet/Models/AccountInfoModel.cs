using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SuperSold.UI.AspDotNet.Models;

public class AccountInfoModel {

    public required string Username { get; set; }
    public required string Email { get; set; }

}

public class UserNameModel {

    [Required(AllowEmptyStrings = false, ErrorMessage = "Please write your new username.")]
    [MinLength(50, ErrorMessage = "too short")]
    [Remote("IsUsernameUnique", "Profile", ErrorMessage = "This username is already in use.")]
    public required string UserName { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Please write a confirmation password.")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

}

public class EmailModel {

    [Required(AllowEmptyStrings = false, ErrorMessage = "Must write a new email.")]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Please write a confirmation password.")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

}