using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperSold.Data.Models;

public class AccountRoleModel {

    [Key]
    public Guid IdAccountRole { get; set; } = Guid.NewGuid();

    [ForeignKey(nameof(AccountModel.IdAccount))]
    public required Guid IdAccount { get; set; }
    public required string Role { get; set; }

}
