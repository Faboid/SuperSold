using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SuperSold.Data.Models;

[Index(nameof(UserName))]
public class AccountModel {

    [Key]
    public Guid IdAccount { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string HashedPassword { get; set; }

}