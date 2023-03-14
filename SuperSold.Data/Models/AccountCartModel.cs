using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperSold.Data.Models;

[Keyless]
public class AccountCartModel {

    [ForeignKey(nameof(AccountModel.IdAccount))]
    public Guid AccountId { get; set; }

    [ForeignKey(nameof(ProductModel.IdProduct))]
    public Guid ProductId { get; set; }

}