using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperSold.Data.Models;

public class AccountRestrictionModel {

    [Key]
    public Guid IdAccountRestriction { get; set; } = Guid.NewGuid();

    [ForeignKey(nameof(AccountModel.IdAccount))]
    public required Guid IdAccount { get; set; }
    public required string Restriction { get; set; }

}