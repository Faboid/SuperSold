namespace SuperSold.Data.Models;

public class ProductModel {

    public Guid IdProduct { get; set; }
    public Guid IdSellerAccount { get; set; }
    public string? ImageUrl { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }

}

