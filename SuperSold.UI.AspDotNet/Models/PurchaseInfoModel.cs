using System.ComponentModel.DataAnnotations;

namespace SuperSold.UI.AspDotNet.Models;

//warningCheckbox=true&fullname=name&city=city&address=address&state=state&postalCode=postalcode&phoneNumber=number&cardNumber=qweqwe&nameCard=name&expirationDate=2023-03

public class PurchaseInfoModel {

    [Required]
    public required bool WarningCheckbox { get; set; }

    [Required(AllowEmptyStrings = false)]
    public required string FullName { get; set; }

    [Required(AllowEmptyStrings = false)]
    public required string City { get; set; }

    [Required(AllowEmptyStrings = false)]
    public required string Address { get; set; }

    [Required(AllowEmptyStrings = false)]
    public required string State { get; set; }

    [Required(AllowEmptyStrings = false)]
    public required string PostalCode { get; set; }

    [Required(AllowEmptyStrings = false)]
    public required string PhoneNumber { get; set; }

    [Required(AllowEmptyStrings = false)]
    public required string CardNumber { get; set; }

    [Required(AllowEmptyStrings = false)]
    public required string NameCard { get; set; }

    [Required]
    public required DateOnly ExpirationDate { get; set; }

}
