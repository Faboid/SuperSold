using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperSold.Data.Models;
public class RollbackModel {

    [Key]
    public Guid IdRollback { get; set; } = Guid.NewGuid();

    [ForeignKey(nameof(AccountModel.IdAccount))]
    public required Guid IdAccount { get; set; }

    [Column(TypeName = "varchar(45)")]
    public required RollbackType RollbackType { get; set; }

    [Column(TypeName = "varchar(250)")]
    public required string Body { get; set; }

    [Column(TypeName = "DATETIME")]
    public required DateOnly ExpireOn { get; set; }

}

public enum RollbackType {
    Password,
    Email,
}
