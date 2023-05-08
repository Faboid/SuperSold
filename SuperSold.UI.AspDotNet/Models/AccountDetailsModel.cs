namespace SuperSold.UI.AspDotNet.Models;

public record AccountDetailsModel(AccountInfoModel Account, IEnumerable<string> Roles, IEnumerable<string> Restrictions, IEnumerable<Product> Products);