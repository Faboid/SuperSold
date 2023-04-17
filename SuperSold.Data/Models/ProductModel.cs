using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SuperSold.Data.Models;

public class ProductModel {

    [Key]
    public Guid IdProduct { get; set; }
    
    [ForeignKey(nameof(AccountModel.IdAccount))]
    public Guid IdSellerAccount { get; set; }

    public string? ImageUrl { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }

}