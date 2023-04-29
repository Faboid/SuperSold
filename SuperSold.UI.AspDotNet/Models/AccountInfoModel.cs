
namespace SuperSold.UI.AspDotNet.Models;

public class AccountInfoModel {

    public Guid? IdAccount { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }

}