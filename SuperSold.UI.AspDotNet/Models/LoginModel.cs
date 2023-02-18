using System.ComponentModel.DataAnnotations;

namespace SuperSold.UI.AspDotNet.Models;

public class LoginModel {

    [Required]
    public required string UserName { get; set; }

    [Required]
    public required string Password { get; set; }

    public bool RememberMe { get; set; }

}
