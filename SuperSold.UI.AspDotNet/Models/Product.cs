using SuperSold.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace SuperSold.UI.AspDotNet.Models;

public class Product {

    [Key]
    public required Guid Id { get; set; }
    public string UserImgUrl { get; set; } = "";

    public required string Title { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }

    public static implicit operator Product(ProductModel model) {
        return new Product() {
            Id = model.IdProduct,
            UserImgUrl = model.ImageUrl ?? "",
            Title = model.Title,
            Description = model.Description,
            Price = model.Price
        };
    }

}
