using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperSold.Data.Models;

[Keyless]
public class AccountWishlistModel {

    [ForeignKey(nameof(AccountModel.IdAccount))]
    public Guid IdAccount { get; set; }
    
    [ForeignKey(nameof(ProductModel.IdProduct))]
    public Guid IdProduct { get; set; }

}