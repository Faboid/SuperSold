namespace SuperSold.Data.Models;

public class AccountModel {

    public Guid IdAccount { get; set; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string HashedPassword { get; set; }

}